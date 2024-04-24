using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MotokaEasy.Core.Infrastructure.Pagination;

namespace MotokaEasy.Core.Infrastructure.Repositories.Contracts;

public interface IRepository<TEntity, TDtoOutPutModel>: IRepositoryBase<TEntity>
{
    Task<TDtoOutPutModel> GetByIdAsync(Guid id, CancellationToken ct);
    Task<TEntity> GetByIdEntityAsync(Guid id, CancellationToken ct);
    Task<PaginationReturn<List<TDtoOutPutModel>>> GetAllPaginationAsync(PaginationParameters paginationParameters, CancellationToken ct);
    Task<TEntity> ExistsAsync(TEntity entity, CancellationToken ct);
    TEntity Exists(TEntity entity);
    Task<List<TDtoOutPutModel>> GetAllByOutPutAsync(CancellationToken ct);

}