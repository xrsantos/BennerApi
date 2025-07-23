using LojaBenner.Contexts;
using LojaBenner.Infrastructure.Repositories.Interfaces;
using LojaBenner.Infrastructure.UnitOfWork.Interfaces;

namespace LojaBenner.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly BennerContext _db;
    public IPessoaRepository Pessoas { get; }
    public IProdutoRepository Produtos { get; }
    public IPedidoRepository Pedidos { get; }

    public UnitOfWork(BennerContext db,
                      IPessoaRepository pessoas,
                      IProdutoRepository produtos,
                      IPedidoRepository pedidos)
    {
        _db = db;
        Pessoas = pessoas;
        Produtos = produtos;
        Pedidos = pedidos;
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);

    public ValueTask DisposeAsync() => _db.DisposeAsync();
}