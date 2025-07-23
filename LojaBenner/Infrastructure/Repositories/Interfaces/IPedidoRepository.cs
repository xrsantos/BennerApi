using LojaBenner.Dtos;
using LojaBenner.Entities;
using LojaBenner.Enums;

namespace LojaBenner.Infrastructure.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        Task AtualizarStatusAsync(int id, StatusPedido novoStatus, CancellationToken ct = default);
        Task<Pedido> CriarPedidoAsync(int pessoaId, IEnumerable<PedidoItemDto>? pedidoItemDtos, FormaPagamento formaPagamento, CancellationToken ct = default);
        Task<Pedido?> GetDetalhadoAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<Pedido>> ListPorPessoaAsync(int pessoaId, CancellationToken ct = default);
    }
}