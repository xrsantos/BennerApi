namespace LojaBenner.Dtos
{
    public record PedidoResumoDto(int Id, string DataVenda, string FormaPagamento, string Status, decimal ValorTotal);
}
