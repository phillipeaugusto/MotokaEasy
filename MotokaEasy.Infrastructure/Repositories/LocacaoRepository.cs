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

public class LocacaoRepository: RepositoryBase<Locacao>, ILocacaoRepository
{
    private readonly DataContext _context;

    public LocacaoRepository(DataContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<LocacaoOutPutDto> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.Locacao
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj?.ConvertToObjectOutPut();
    }

    public async Task<Locacao> GetByIdEntityAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.Locacao
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj;
    }

    public async Task<PaginationReturn<List<LocacaoOutPutDto>>> GetAllPaginationAsync(PaginationParameters paginationParameters, CancellationToken ct)
    {
        return await new PaginationReturnRepository<Locacao, LocacaoOutPutDto>(paginationParameters, _context.Locacao,
                x => x.ConvertToObjectOutPut())
            .ReturnDataPagination(x => true);
    }

    public async Task<Locacao> ExistsAsync(Locacao entity, CancellationToken ct)
    {
        return await _context.Locacao.AsNoTracking()
            .FirstOrDefaultAsync(x => x.PlanoId == entity.PlanoId && 
                                      x.DataInicio == entity.DataInicio &&
                                      x.DataTerminio == entity.DataTerminio && 
                                      x.DataPrevisaoTerminio == entity.DataPrevisaoTerminio, ct);
    }

    public Locacao Exists(Locacao entity)
    {
        return _context.Locacao.AsNoTracking()
            .FirstOrDefault(x => x.PlanoId == entity.PlanoId && 
                                      x.DataInicio == entity.DataInicio &&
                                      x.DataTerminio == entity.DataTerminio && 
                                      x.DataPrevisaoTerminio == entity.DataPrevisaoTerminio);

    }

    public async Task<List<LocacaoOutPutDto>> GetAllByOutPutAsync(CancellationToken ct)
    {
        return await _context.Locacao
            .AsNoTracking()
            .Select(x => x.ConvertToObjectOutPut())
            .ToListAsync(ct);
    }

    public async Task<bool> ValidarSeVeiculoPossuiRegistroDeLocacaoAsync(Guid veiculoId, CancellationToken ct)
    {
        var objCount = await _context.Locacao
            .AsNoTracking()
            .CountAsync(x => x.VeiculoId == veiculoId, ct);

        return objCount >= 1;
    }
}