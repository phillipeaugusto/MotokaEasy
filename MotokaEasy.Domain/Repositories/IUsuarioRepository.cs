using System.Threading;
using System.Threading.Tasks;
using MotokaEasy.Core.Infrastructure.Repositories.Contracts;
using MotokaEasy.Domain.Dto.OutPutDto;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Domain.Repositories;

public interface IUsuarioRepository: IRepository<Usuario, UsuarioOutPutDto>
{
    Task<bool> ValidarSeEmailEstaCadastradoAsync(string email, CancellationToken ct);
}