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

public class EntregadorRepository: RepositoryBase<Entregador>, IEntregadorRepository
{
    private readonly DataContext _context;

    public EntregadorRepository(DataContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<EntregadorOutPutDto> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.Entregador
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj?.ConvertToObjectOutPut();
    }

    public async Task<Entregador> GetByIdEntityAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.Entregador
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj;
    }

    public async Task<PaginationReturn<List<EntregadorOutPutDto>>> GetAllPaginationAsync(PaginationParameters paginationParameters, CancellationToken ct)
    {
        return await new PaginationReturnRepository<Entregador, EntregadorOutPutDto>(paginationParameters, _context.Entregador,
                x => x.ConvertToObjectOutPut())
            .ReturnDataPagination(x => true);
    }

    public async Task<Entregador> ExistsAsync(Entregador entity, CancellationToken ct)
    {
        return await _context.Entregador
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CnpjCpf == entity.CnpjCpf || x.NumeroCnh == entity.NumeroCnh, ct);
    }

    public Entregador Exists(Entregador entity)
    {
        return _context.Entregador
            .AsNoTracking()
            .FirstOrDefault(x => x.CnpjCpf == entity.CnpjCpf || x.NumeroCnh == entity.NumeroCnh);
    }

    public async Task<List<EntregadorOutPutDto>> GetAllByOutPutAsync(CancellationToken ct)
    {
        return await _context.Entregador
            .AsNoTracking()
            .Select(x => x.ConvertToObjectOutPut())
            .ToListAsync(ct);
    }

    public async Task<bool> ValidarSeCnhEstaVinculadaAlgumEntregadorAsync(string cnh, CancellationToken ct)
    {
        var objCount = await _context.Entregador
            .AsNoTracking()
            .CountAsync(x => x.NumeroCnh == cnh, ct);
        
        return objCount >= 1;
    }

    public async Task AtualizarUrlCnhEntregadorAsync(Guid id, string url, CancellationToken ct)
    {
        var obj = await GetByIdEntityAsync(id, ct);
        obj.ImagemCnh = url;
        await UpdateAsync(obj, ct);
    }

    public async Task<bool> ValidarSeCnpjCpfEstaVinculadoAlgumEntregadorAsync(string cnpjCpf, CancellationToken ct)
    {
        var objCount = await _context.Entregador
            .AsNoTracking()
            .CountAsync(x => x.CnpjCpf == cnpjCpf, ct);
        
        return objCount >= 1;
    }
}