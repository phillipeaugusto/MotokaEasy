using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using MotokaEasy.Core.Infrastructure.Cloud.Aws.Config;
using MotokaEasy.Core.Infrastructure.Cloud.Contracts;
using MotokaEasy.Core.Infrastructure.Cloud.Storage;

namespace MotokaEasy.Core.Infrastructure.Cloud.Aws;

public class AwsCloud: ICloud
{
    private readonly IAmazonS3 _awsS3Client;

    public AwsCloud(AwsConfig awsConfig)
    {
        _awsS3Client = awsConfig.Production ? new AmazonS3Client(new BasicAWSCredentials(awsConfig.AccessKey, awsConfig.SecretKey), RegionEndpoint.USEast1) : new AmazonS3Client(new AmazonS3Config { UseHttp = true, ServiceURL = awsConfig.Url, ForcePathStyle = true});
    }
    public async Task<StorageReturn> UploadFileAsync(string folder, string fileName, Stream file)
    {
       
        var objReturn = new StorageReturn();
        
        try
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = file,
                Key = fileName,
                BucketName = folder
            };

            var fileTransferUtility = new TransferUtility(_awsS3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);
            
            objReturn.Sucesso = true;
            objReturn.UrlArquivo = $"https://{folder}.s3.amazonaws.com/{fileName}";
            objReturn.Mensagem =  "Upload Ok";
        }
        catch (Exception e)
        {
            objReturn.Mensagem = e.Message;
            objReturn.Sucesso = false;
            objReturn.UrlArquivo = string.Empty;
        }
        return objReturn;
    }

    public async Task<MemoryStream> DownloadFileAsync(string folder, string fileName)
    {
        var memoryStream = new MemoryStream();
        var getObjectRequest = new GetObjectRequest
        {
            BucketName = folder,
            Key = fileName
        };

        using var response = await _awsS3Client.GetObjectAsync(getObjectRequest);
        if (response.HttpStatusCode == HttpStatusCode.OK)
            await response.ResponseStream.CopyToAsync(memoryStream);
    
        return memoryStream;
    }
}