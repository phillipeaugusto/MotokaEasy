using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MotokaEasy.Core.Infrastructure.Pagination;
using MotokaEasy.Core.Infrastructure.Repositories;
using MotokaEasy.Domain.Entities;
using MotokaEasy.Domain.Repositories;
using MotokaEasy.Infrastructure.Contexts;

namespace MotokaEasy.Infrastructure.Repositories;

public class ConfiguracaoRepository: RepositoryBase<Configuracao>, IConfiguracaoRepository
{
    private readonly DataContext _context;

    public ConfiguracaoRepository(DataContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<Configuracao> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.Configuracao
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj?.ConvertToObjectOutPut();
    }

    public async Task<Configuracao> GetByIdEntityAsync(Guid id, CancellationToken ct)
    {
        var obj = await _context.Configuracao
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return obj;
    }

    public async Task<PaginationReturn<List<Configuracao>>> GetAllPaginationAsync(PaginationParameters paginationParameters, CancellationToken ct)
    {
        return await new PaginationReturnRepository<Configuracao, Configuracao>(paginationParameters, _context.Configuracao,
                x => x.ConvertToObjectOutPut())
            .ReturnDataPagination(x => true);
    }

    public async Task<Configuracao> ExistsAsync(Configuracao entity, CancellationToken ct)
    {
        return await _context.Configuracao
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CodigoParametro == entity.CodigoParametro, ct);
    }

    public Configuracao Exists(Configuracao entity)
    {
        return _context.Configuracao
            .AsNoTracking()
            .FirstOrDefault(x => x.CodigoParametro == entity.CodigoParametro);
    }

    public async Task<List<Configuracao>> GetAllByOutPutAsync(CancellationToken ct)
    {
        return await _context.Configuracao
            .AsNoTracking()
            .Select(x => x.ConvertToObjectOutPut())
            .ToListAsync(ct);
    }

    public async Task<string> RetornarValorParametroAsync(int codigoParametro, CancellationToken ct)
    {
        var obj = await _context.Configuracao
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CodigoParametro == codigoParametro, ct);
        
        return obj.Valor;
    }
}