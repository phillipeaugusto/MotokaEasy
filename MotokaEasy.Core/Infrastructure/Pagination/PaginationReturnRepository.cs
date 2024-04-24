using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MotokaEasy.Core.Infrastructure.Pagination;

[ExcludeFromCodeCoverage]
public class PaginationReturnRepository<TEntity, TDtoOutPutModel> where TEntity : class 
{
    private readonly PaginationParameters _paginationParameters;
    private readonly DbSet<TEntity> _dbSet;
    private readonly Expression<Func<TEntity, TDtoOutPutModel>> _select;
        
    public PaginationReturnRepository(PaginationParameters paginationParameters, DbSet<TEntity> dbSet, Expression<Func<TEntity, TDtoOutPutModel>> select)
    {
        _paginationParameters = paginationParameters;
        _dbSet = dbSet;
        _select = select;
    }
        
    public async Task<PaginationReturn<List<TDtoOutPutModel>>> ReturnDataPagination<TKey>(Expression<Func<TEntity, TKey>> orderBy = null)
    {
        var pagedData = await _dbSet
            .AsNoTracking()
            .Skip(PaginationBusiness.CalculateSkip(_paginationParameters))
            .Take(_paginationParameters.PageSize)
            .OrderBy(orderBy!)
            .Select(_select)
            .ToListAsync();
        
        var recordCount = _dbSet.Count();
        var pagesRemaining = PaginationBusiness.CalculateCountPage(recordCount, _paginationParameters.PageSize);
        return new PaginationReturn<List<TDtoOutPutModel>>(_paginationParameters.PageNumber, _paginationParameters.PageSize, recordCount , pagedData , pagesRemaining);
    }
}