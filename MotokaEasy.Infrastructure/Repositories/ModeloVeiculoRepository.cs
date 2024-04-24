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

public class ModeloVeiculoRepository: RepositoryBase<ModeloVeiculo>, IModeloVeiculoRepository
{
    private readonly DataContext _context;

    public ModeloVeiculoRepository(DataContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<ModeloVeiculoOutPutDto> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.ModeloVeiculo
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj?.ConvertToObjectOutPut();
    }

    public async Task<ModeloVeiculo> GetByIdEntityAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.ModeloVeiculo
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj;
    }

    public async Task<PaginationReturn<List<ModeloVeiculoOutPutDto>>> GetAllPaginationAsync(PaginationParameters paginationParameters, CancellationToken ct)
    {
        return await new PaginationReturnRepository<ModeloVeiculo, ModeloVeiculoOutPutDto>(paginationParameters, _context.ModeloVeiculo,
                x => x.ConvertToObjectOutPut())
            .ReturnDataPagination(x => true);
    }

    public async Task<ModeloVeiculo> ExistsAsync(ModeloVeiculo entity, CancellationToken ct)
    {
        return await _context.ModeloVeiculo
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Descricao.Equals(entity.Descricao), ct);
    }

    public ModeloVeiculo Exists(ModeloVeiculo entity)
    {
        return _context.ModeloVeiculo
            .AsNoTracking()
            .FirstOrDefault(x => x.Descricao.Equals(entity.Descricao));

    }

    public async Task<List<ModeloVeiculoOutPutDto>> GetAllByOutPutAsync(CancellationToken ct)
    {
        return await _context.ModeloVeiculo
            .AsNoTracking()
            .Select(x => x.ConvertToObjectOutPut())
            .ToListAsync(ct);
    }
}