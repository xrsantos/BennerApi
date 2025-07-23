using LojaBenner.Contexts;
using LojaBenner.Dtos;
using LojaBenner.Entities;
using LojaBenner.Enums;
using LojaBenner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaBenner.Infrastructure.Repositories;

public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
{
    public PedidoRepository(BennerContext db) : base(db) { }

    public async Task<Pedido?> GetDetalhadoAsync(int id, CancellationToken ct = default) =>
        await _setEntitie
            .Include(p => p.Pessoa)
            .Include(p => p.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<IReadOnlyList<Pedido>> ListPorPessoaAsync(int pessoaId, CancellationToken ct = default) =>
        await _setEntitie.AsNoTracking().Where(p => p.PessoaId == pessoaId)
            .Include(p => p.Itens)
            .OrderByDescending(p => p.DataVenda)
            .ToListAsync(ct);

    public async Task AtualizarStatusAsync(int id, StatusPedido novoStatus, CancellationToken ct = default)
    {
        var pedido = await _setEntitie.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (pedido is null) throw new KeyNotFoundException($"Pedido {id} não encontrado.");
        pedido.Status = novoStatus; 
        _db.Entry(pedido).Property(p => p.Status).IsModified = true;
    }

    public async Task<Pedido> CriarPedidoAsync(int pessoaId, IEnumerable<PedidoItemDto>? pedidoItemDtos, FormaPagamento formaPagamento, CancellationToken ct = default)
    {
        if (pedidoItemDtos is null || !pedidoItemDtos.Any()) throw new ArgumentException("É necessário pelo menos 1 item.", nameof(pedidoItemDtos));

        // Carrega produtos para snapshot de preço
        var ids = pedidoItemDtos.Select(i => i.ProdutoId).Distinct().ToList();
        var produtos = await _db.Produtos.Where(p => ids.Contains(p.Id)).ToListAsync(ct);
        if (produtos.Count != ids.Count)
            throw new InvalidOperationException("Um ou mais produtos não existem.");

        var pedido = new Pedido
        {
            PessoaId = pessoaId,
            FormaPagamento = formaPagamento,
            Status = StatusPedido.Pendente,
            DataVenda = DateTime.UtcNow
        };

        foreach (var (produtoId, qtd) in pedidoItemDtos)
        {
            if (qtd <= 0) throw new ArgumentOutOfRangeException(nameof(pedidoItemDtos), "Quantidade deve ser > 0.");
            var prod = produtos.Single(p => p.Id == produtoId);
            pedido.Itens.Add(new PedidoItem
            {
                ProdutoId = prod.Id,
                Quantidade = qtd,
                PrecoUnitario = prod.Valor
            });
        }

        pedido.RecalcularTotal();
        await _setEntitie.AddAsync(pedido, ct);
        return pedido;
    }
}
