using System.IO;
using System.Threading.Tasks;

namespace MotokaEasy.Core.Infrastructure.Cloud.Storage.Contracts;

public interface ICloudStorage
{
    Task<StorageReturn> UploadFileAsync(string folder, string fileName, Stream file);
    Task<MemoryStream> DownloadFileAsync(string folder, string fileName);
}