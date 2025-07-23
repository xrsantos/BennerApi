using LojaBenner.Enums;

namespace LojaBenner.Entities;

public class Pedido
{
    public int Id { get; set; }
    public int PessoaId { get; set; }
    public Pessoa Pessoa { get; set; } = null!;
    public List<PedidoItem> Itens { get; set; } = new();
    public decimal ValorTotal { get;  set; }          // calculado
    public DateTime DataVenda { get;  set; } = DateTime.UtcNow;
    public FormaPagamento FormaPagamento { get; set; }
    public StatusPedido Status { get;  set; } = StatusPedido.Pendente;
    public void RecalcularTotal()
        => ValorTotal = Itens.Sum(i => i.Quantidade * i.PrecoUnitario);
}
