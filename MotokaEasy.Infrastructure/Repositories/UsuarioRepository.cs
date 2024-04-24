using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MotokaEasy.Core.Infrastructure.Pagination;
using MotokaEasy.Core.Infrastructure.Repositories;
using MotokaEasy.Domain.Dto.OutPutDto;
using MotokaEasy.Domain.Entities;
using MotokaEasy.Domain.Repositories;
using MotokaEasy.Infrastructure.Contexts;

namespace MotokaEasy.Infrastructure.Repositories;

public class UsuarioRepository: RepositoryBase<Usuario>, IUsuarioRepository
{
    private readonly DataContext _context;

    public UsuarioRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UsuarioOutPutDto> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.Usuario
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj?.ConvertToObjectOutPut();
    }

    public async Task<Usuario> GetByIdEntityAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.Usuario
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj;
    }

    public async Task<PaginationReturn<List<UsuarioOutPutDto>>> GetAllPaginationAsync(
        PaginationParameters paginationParameters, CancellationToken ct)
    {
        return await new PaginationReturnRepository<Usuario, UsuarioOutPutDto>(paginationParameters, _context.Usuario,
                x => x.ConvertToObjectOutPut())
            .ReturnDataPagination(x => true);
    }

    public async Task<Usuario> ExistsAsync(Usuario entity, CancellationToken ct)
    {
        return await _context.Usuario
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Equals(entity.Email), ct);
    }

    public Usuario Exists(Usuario entity)
    {
        return _context.Usuario
            .AsNoTracking()
            .FirstOrDefault(x => x.Email.Equals(entity.Email));
    }

    public async Task<List<UsuarioOutPutDto>> GetAllByOutPutAsync(CancellationToken ct)
    {
        return await _context.Usuario
            .AsNoTracking()
            .Select(x => x.ConvertToObjectOutPut())
            .ToListAsync(ct);
    }

    public async Task<bool> ValidarSeEmailEstaCadastradoAsync(string email, CancellationToken ct)
    {
        var objCount = await _context.Usuario
            .AsNoTracking()
            .CountAsync(x => x.Email.Equals(email), ct);

        return objCount >= 1;
    }
}
