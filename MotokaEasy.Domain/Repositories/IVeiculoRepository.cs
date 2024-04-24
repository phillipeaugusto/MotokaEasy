using System;
using System.Threading;
using System.Threading.Tasks;
using MotokaEasy.Core.Infrastructure.Repositories.Contracts;
using MotokaEasy.Domain.Dto.OutPutDto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Repositories;

public interface IVeiculoRepository: IRepository<Veiculo, VeiculoOutPutDto>
{
    Task<bool> ValidarSePlacaJaExisteAsync(string placa, CancellationToken ct);
    Task InativarCadastroAsync(Guid id, CancellationToken ct);
    Task<VeiculoOutPutDto> BuscarVeiculoPelaPlacaAsync(string placa, CancellationToken ct);
    Task AtualizarPlacaVeiculoAsync(Guid veiculoId, string placa, CancellationToken ct);
}