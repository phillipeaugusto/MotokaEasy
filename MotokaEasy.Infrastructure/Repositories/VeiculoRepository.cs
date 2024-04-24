using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MotokaEasy.Core.ConstantsApp;
using MotokaEasy.Core.Infrastructure.Pagination;
using MotokaEasy.Core.Infrastructure.Repositories;
using MotokaEasy.Domain.Dto.OutPutDto;
using MotokaEasy.Domain.Entities;
using MotokaEasy.Domain.Repositories;
using MotokaEasy.Infrastructure.Contexts;

namespace MotokaEasy.Infrastructure.Repositories;

public class VeiculoRepository: RepositoryBase<Veiculo>, IVeiculoRepository
{
    private readonly DataContext _context;

    public VeiculoRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<VeiculoOutPutDto> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.Veiculo
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj?.ConvertToObjectOutPut();
    }

    public async Task<Veiculo> GetByIdEntityAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.Veiculo
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj;
    }

    public async Task<PaginationReturn<List<VeiculoOutPutDto>>> GetAllPaginationAsync(
        PaginationParameters paginationParameters, CancellationToken ct)
    {
        return await new PaginationReturnRepository<Veiculo, VeiculoOutPutDto>(paginationParameters, _context.Veiculo,
                x => x.ConvertToObjectOutPut())
            .ReturnDataPagination(x => true);
    }

    public async Task<Veiculo> ExistsAsync(Veiculo entity, CancellationToken ct)
    {
        return await _context.Veiculo
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Placa.Equals(entity.Placa), ct);
    }

    public Veiculo Exists(Veiculo entity)
    {
        return _context.Veiculo
            .AsNoTracking()
            .FirstOrDefault(x => x.Placa.Equals(entity.Placa));
    }

    public async Task<List<VeiculoOutPutDto>> GetAllByOutPutAsync(CancellationToken ct)
    {
        return await _context.Veiculo
            .AsNoTracking()
            .Select(x => x.ConvertToObjectOutPut())
            .ToListAsync(ct);
    }

    public async Task<bool> ValidarSePlacaJaExisteAsync(string placa, CancellationToken ct)
    {
        var count = await _context.Veiculo
            .AsNoTracking()
            .CountAsync(x => x.Placa == placa, ct);

        return count >= 1;
    }

    public async Task InativarCadastroAsync(Guid id, CancellationToken ct)
    {
        var obj = await GetByIdEntityAsync(id, ct);
        obj.Status = ApplicationConstants.StatusInativo;
        await UpdateAsync(obj, ct);
    }

    public async Task<VeiculoOutPutDto> BuscarVeiculoPelaPlacaAsync(string placa, CancellationToken ct)
    {
        return await _context.Veiculo
            .AsNoTracking()
            .Where(x => x.Placa.Equals(placa))
            .Select(x => x.ConvertToObjectOutPut())
            .FirstOrDefaultAsync(ct);
    }

    public async Task AtualizarPlacaVeiculoAsync(Guid veiculoId, string placa, CancellationToken ct)
    {
        var obj = await GetByIdEntityAsync(veiculoId, ct);
        obj.Placa = placa;
        await UpdateAsync(obj, ct);
    }
}
