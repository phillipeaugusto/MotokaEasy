using System.Diagnostics.CodeAnalysis;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;

namespace MotokaEasy.Core.Domain.Dto;

[ExcludeFromCodeCoverage]
public abstract class DtoBase<TEntity, TDtoOutPutModel>
{
    private readonly Mapper _mapperSource;
    private readonly Mapper _mapperDestiny;

    protected DtoBase()
    {
        var mapperConfigurationSource = new MapperConfiguration(cfg =>
            cfg.CreateMap<TEntity, TDtoOutPutModel>()
        );

        _mapperSource = new Mapper(mapperConfigurationSource);
            
        var mapperConfigurationDestiny = new MapperConfiguration(cfg =>
            cfg.CreateMap<TEntity, TDtoOutPutModel>()
        );

        _mapperDestiny = new Mapper(mapperConfigurationDestiny);
            
    }
    public TDtoOutPutModel ConvertToObject() => _mapperSource.Map<TDtoOutPutModel>(this);
    public TEntity ConvertToObjecTEntity() => _mapperDestiny.Map<TEntity>(this);

    public string ConvertToJson(bool convertToObject)
    {
        return convertToObject switch
        {
            true => JsonConvert.SerializeObject(ConvertToObject()),
            _ => JsonConvert.SerializeObject(this)
        };
    }
    public string ConvertToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
        
    public byte[] ConvertToByte()
    {
        return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
    }
        
}