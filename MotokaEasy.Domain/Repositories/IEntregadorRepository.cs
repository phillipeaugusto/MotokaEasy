using System;
using System.Threading;
using System.Threading.Tasks;
using MotokaEasy.Core.Infrastructure.Repositories.Contracts;
using MotokaEasy.Domain.Dto.OutPutDto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Repositories;

public interface IEntregadorRepository: IRepository<Entregador, EntregadorOutPutDto>
{
    Task<bool> ValidarSeCnhEstaVinculadaAlgumEntregadorAsync(string cnh, CancellationToken ct);
    Task AtualizarUrlCnhEntregadorAsync(Guid id, string url, CancellationToken ct);
    Task<bool> ValidarSeCnpjCpfEstaVinculadoAlgumEntregadorAsync(string cnpjCpf, CancellationToken ct);
}