using LojaBenner.Contexts;
using LojaBenner.Entities;
using LojaBenner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaBenner.Infrastructure.Repositories
{
    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(BennerContext db) : base(db) { }


        public Task<Pessoa?> GetByIdAsync(int id, CancellationToken ct = default)
            => _setEntitie.FirstOrDefaultAsync(p => p.Id == id, ct);

        public async Task<IReadOnlyList<Pessoa>> SearchAsync(int skip = 0, int take = 50, CancellationToken ct = default)
        {
            var q = _setEntitie.AsNoTracking().AsQueryable();

            return await q.OrderBy(p => p.Nome).Skip(skip).Take(take).ToListAsync(ct);
        }

        public async Task<Pessoa?> Create(Pessoa Pessoa, CancellationToken ct = default)
        {
            await _setEntitie.AddAsync(Pessoa, ct);
            return Pessoa;
        }

        public Pessoa Edit(Pessoa Pessoa)
        {
            _setEntitie.Update(Pessoa);
            return Pessoa;
        }

        public Pessoa Delete(Pessoa Pessoa)
        {
            _setEntitie.Remove(Pessoa);
            return Pessoa;
        }

    }
}
