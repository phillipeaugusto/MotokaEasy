using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MotokaEasy.Core.Infrastructure.Repositories.Contracts;

public interface IRepositoryBase<TEntity>
{
    Task CreateAsync(TEntity entity, CancellationToken ct);
    Task UpdateAsync(TEntity entity, CancellationToken ct);
    Task DeleteAsync(TEntity entity, CancellationToken ct);
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task CreateListAsync(IEnumerable<TEntity> list, CancellationToken ct);
    Task UpdateListAsync(IEnumerable<TEntity> list, CancellationToken ct);
    Task DeleteListAsync(IEnumerable<TEntity> list, CancellationToken ct);
    List<TEntity> GetAll();
    Task<List<TEntity>> GetAllAsync(CancellationToken ct);
    
}