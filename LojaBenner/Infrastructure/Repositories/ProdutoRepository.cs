using LojaBenner.Contexts;
using LojaBenner.Entities;
using LojaBenner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaBenner.Infrastructure.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(BennerContext db) : base(db) { }


        public Task<Produto?> GetByIdAsync(int id, CancellationToken ct = default)
            => _setEntitie.FirstOrDefaultAsync(p => p.Id == id, ct);
        public Task<Produto?> GetByCodigoAsync(string codigo, CancellationToken ct = default)
            => _setEntitie.FirstOrDefaultAsync(p => p.Codigo == codigo, ct);

        public async Task<IReadOnlyList<Produto>> SearchAsync(string? nome, string? codigo, decimal? valorMin, decimal? valorMax, int skip = 0, int take = 50, CancellationToken ct = default)
        {
            var q = _setEntitie.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(nome))
                q = q.Where(p => p.Nome.Contains(nome));

            if (!string.IsNullOrWhiteSpace(codigo))
                q = q.Where(p => p.Codigo.Contains(codigo));

            if (valorMin.HasValue) q = q.Where(p => p.Valor >= valorMin.Value);
            if (valorMax.HasValue) q = q.Where(p => p.Valor <= valorMax.Value);
            return await q.OrderBy(p => p.Nome).Skip(skip).Take(take).ToListAsync(ct);
        }

        public async Task<Produto?> Create(Produto produto, CancellationToken ct = default)
        {
            await _setEntitie.AddAsync(produto, ct);
            return produto;
        }

        public Produto Edit(Produto produto)
        {
            _setEntitie.Update(produto);
            return produto;
        }

        public Produto Delete(Produto produto)
        {
            _setEntitie.Remove(produto);
            return produto;
        }

    }
}
