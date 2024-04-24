using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MotokaEasy.Core.Infrastructure.Repositories.Contracts;

namespace MotokaEasy.Core.Infrastructure.Repositories;

public class RepositoryBase<T>: IDisposable, IRepositoryBase<T> where T: class
    {
        private readonly DbContext _context;
        public RepositoryBase(DbContext context)
        {
            _context = context;
        }
       
        public async Task CreateAsync(T entity, CancellationToken ct)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public virtual Task<List<T>> GetAllAsync(CancellationToken ct)
        {
            return _context.Set<T>().AsNoTracking().ToListAsync(ct);
        }

        public virtual List<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public void DetachLocal(Func<T, bool> predicate)
        {
            var obj = _context.Set<T>().Local.Where(predicate).FirstOrDefault();
            if (obj is {})
                _context.Entry(obj).State = EntityState.Detached;
        }
        
        public virtual async Task UpdateAsync(T entity, CancellationToken ct)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(ct);
        }

        public virtual async Task DeleteAsync(T entity, CancellationToken ct)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(ct);
        }

        public virtual void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual async Task CreateListAsync(IEnumerable<T> list, CancellationToken ct)
        {
            _context.Set<T>().AddRange(list);
            await _context.SaveChangesAsync(ct);
        }

        public virtual async Task UpdateListAsync(IEnumerable<T> list, CancellationToken ct)
        {
            foreach (var obj in list) 
                _context.Entry(obj).State = EntityState.Modified;
            
            await _context.SaveChangesAsync(ct);
        }

        public virtual async Task DeleteListAsync(IEnumerable<T> list, CancellationToken ct)
        {
            _context.Set<T>().RemoveRange(list);
            await _context.SaveChangesAsync(ct);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }