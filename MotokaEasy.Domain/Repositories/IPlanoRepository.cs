using MotokaEasy.Core.Infrastructure.Repositories.Contracts;
using MotokaEasy.Domain.Dto.OutPutDto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Repositories;

public interface IPlanoRepository: IRepository<Plano, PlanoOutPutDto>
{
    
}