using System.Linq.Expressions;
using LojaBenner.Contexts;
using LojaBenner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaBenner.Infrastructure.Repositories;

public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    protected readonly BennerContext _db;
    protected readonly DbSet<T> _setEntitie;

    protected RepositoryBase(BennerContext db)
    {
        _db = db;
        _setEntitie = _db.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _setEntitie.FindAsync([id], ct);

    public virtual async Task<IReadOnlyList<T>> ListAsync(CancellationToken ct = default)
        => await _setEntitie.AsNoTracking().ToListAsync(ct);

    public virtual async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        => await _setEntitie.AsNoTracking().Where(predicate).ToListAsync(ct);

    public virtual async Task AddAsync(T entity, CancellationToken ct = default)
        => await _setEntitie.AddAsync(entity, ct);

    public virtual void Update(T entity) => _setEntitie.Update(entity);
    public virtual void Remove(T entity) => _setEntitie.Remove(entity);
}
