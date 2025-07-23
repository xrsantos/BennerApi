using LojaBenner.Entities;

namespace LojaBenner.Infrastructure.Repositories.Interfaces
{
    public interface IPessoaRepository
    {
        Task<Pessoa?> Create(Pessoa Pessoa, CancellationToken ct = default);
        Pessoa Delete(Pessoa Pessoa);
        Pessoa Edit(Pessoa Pessoa);
        Task<Pessoa?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<Pessoa>> SearchAsync(int skip = 0, int take = 50, CancellationToken ct = default);
    }
}