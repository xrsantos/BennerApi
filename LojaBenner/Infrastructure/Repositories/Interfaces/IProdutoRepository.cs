using LojaBenner.Entities;

namespace LojaBenner.Infrastructure.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto?> Create(Produto produto, CancellationToken ct = default);
        Produto Delete(Produto produto);
        Produto Edit(Produto produto);
        Task<Produto?> GetByCodigoAsync(string codigo, CancellationToken ct = default);
        Task<Produto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<Produto>> SearchAsync(string? nome, string? codigo, decimal? valorMin, decimal? valorMax, int skip = 0, int take = 50, CancellationToken ct = default);
    }
}