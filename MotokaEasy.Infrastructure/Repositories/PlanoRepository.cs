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

public class PlanoRepository: RepositoryBase<Plano>, IPlanoRepository
{
    private readonly DataContext _context;

    public PlanoRepository(DataContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<PlanoOutPutDto> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.Plano
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj?.ConvertToObjectOutPut();
    }

    public async Task<Plano> GetByIdEntityAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.Plano
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj;
    }

    public async Task<PaginationReturn<List<PlanoOutPutDto>>> GetAllPaginationAsync(PaginationParameters paginationParameters, CancellationToken ct)
    {
        return await new PaginationReturnRepository<Plano, PlanoOutPutDto>(paginationParameters, _context.Plano,
                x => x.ConvertToObjectOutPut())
            .ReturnDataPagination(x => true);
    }

    public async Task<Plano> ExistsAsync(Plano entity, CancellationToken ct)
    {
        return await _context.Plano
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Descricao.Equals(entity.Descricao) && x.QuantidadeDias == entity.QuantidadeDias, ct);
    }

    public Plano Exists(Plano entity)
    {
        return _context.Plano
            .AsNoTracking()
            .FirstOrDefault(x => x.Descricao.Equals(entity.Descricao) && x.QuantidadeDias == entity.QuantidadeDias);
    }

    public async Task<List<PlanoOutPutDto>> GetAllByOutPutAsync(CancellationToken ct)
    {
        return await _context.Plano
            .AsNoTracking()
            .Select(x => x.ConvertToObjectOutPut())
            .ToListAsync(ct);
    }
}