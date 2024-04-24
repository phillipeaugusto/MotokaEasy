using MotokaEasy.Core.Infrastructure.Repositories.Contracts;
using MotokaEasy.Domain.Dto.OutPutDto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Repositories;

public interface ITipoVeiculoRepository: IRepository<TipoVeiculo, TipoVeiculoOutPutDto>
{
    
}