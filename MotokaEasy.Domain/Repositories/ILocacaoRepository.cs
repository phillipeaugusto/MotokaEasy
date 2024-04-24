using System;
using System.Threading;
using System.Threading.Tasks;
using MotokaEasy.Core.Infrastructure.Repositories.Contracts;
using MotokaEasy.Domain.Dto.OutPutDto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Repositories;

public interface ILocacaoRepository: IRepository<Locacao, LocacaoOutPutDto>
{
    Task<bool> ValidarSeVeiculoPossuiRegistroDeLocacaoAsync(Guid veiculoId, CancellationToken ct);
}