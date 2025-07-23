using LojaBenner.Infrastructure.Repositories.Interfaces;

namespace LojaBenner.Infrastructure.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IPedidoRepository Pedidos { get; }
        IPessoaRepository Pessoas { get; }
        IProdutoRepository Produtos { get; }

        ValueTask DisposeAsync();
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}