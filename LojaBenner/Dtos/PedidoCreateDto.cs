using LojaBenner.Entities;
using LojaBenner.Enums;

namespace LojaBenner.Dtos
{
    public class PedidoCreateDto
    {
        public int PessoaId { get; set; }
        public IEnumerable<PedidoItemDto>? Itens { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
    }

    public record PedidoItemDto(int ProdutoId, int Quantidade);
}
