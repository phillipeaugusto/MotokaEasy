using System.Text;
using AutoMapper;
using Newtonsoft.Json;

namespace MotokaEasy.Core.Domain.Entities;

public abstract class EntityBase<TEntity, TDtoOutPutModel, TOutPut> 
{
    private readonly Mapper _mapperTDtoOutPutModel;
    private readonly Mapper _mapperTOutPut;

    protected EntityBase()
    {
        var mapperConfigurationSource = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, TDtoOutPutModel>());
        _mapperTDtoOutPutModel = new Mapper(mapperConfigurationSource);
        var mapperConfigurationDestiny= new MapperConfiguration(cfg => cfg.CreateMap<TEntity, TOutPut>());
        _mapperTOutPut = new Mapper(mapperConfigurationDestiny);
    }
    public TEntity LoadFromObject<TObject>(TObject obj)
    {
        var mapperConfigurationGeneric = new MapperConfiguration(cfg => cfg.CreateMap<TObject, TEntity>());
        var mapperGeneric = new Mapper(mapperConfigurationGeneric);
        return mapperGeneric.Map<TEntity>(obj);
    }
    public TDtoOutPutModel ConvertToObject() => _mapperTDtoOutPutModel.Map<TDtoOutPutModel>(this);
    public TOutPut ConvertToObjectOutPut() => _mapperTOutPut.Map<TOutPut>(this);

    public string ConvertToJson(bool convertToObjectOutPut)
    {
        return convertToObjectOutPut switch
        {
            true => JsonConvert.SerializeObject(ConvertToObjectOutPut()),
            _ => JsonConvert.SerializeObject(this)
        };
    }

    public byte[] ConvertToByte()
    {
        return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
    }
        
}