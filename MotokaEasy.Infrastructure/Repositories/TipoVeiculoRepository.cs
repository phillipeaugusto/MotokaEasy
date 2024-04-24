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

public class TipoVeiculoRepository: RepositoryBase<TipoVeiculo>, ITipoVeiculoRepository
{
    private readonly DataContext _context;

    public TipoVeiculoRepository(DataContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<TipoVeiculoOutPutDto> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.TipoVeiculo
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj?.ConvertToObjectOutPut();
    }

    public async Task<TipoVeiculo> GetByIdEntityAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.TipoVeiculo
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj;
    }

    public async Task<PaginationReturn<List<TipoVeiculoOutPutDto>>> GetAllPaginationAsync(PaginationParameters paginationParameters, CancellationToken ct)
    {
        return await new PaginationReturnRepository<TipoVeiculo, TipoVeiculoOutPutDto>(paginationParameters, _context.TipoVeiculo,
                x => x.ConvertToObjectOutPut())
            .ReturnDataPagination(x => true);
    }

    public async Task<TipoVeiculo> ExistsAsync(TipoVeiculo entity, CancellationToken ct)
    {
        return await _context.TipoVeiculo
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Descricao.Equals(entity.Descricao), ct);
    }

    public TipoVeiculo Exists(TipoVeiculo entity)
    {
        return _context.TipoVeiculo
            .AsNoTracking()
            .FirstOrDefault(x => x.Descricao.Equals(entity.Descricao));
    }


    public async Task<List<TipoVeiculoOutPutDto>> GetAllByOutPutAsync(CancellationToken ct)
    {
        return await _context.TipoVeiculo
            .AsNoTracking()
            .Select(x => x.ConvertToObjectOutPut())
            .ToListAsync(ct);
    }
}