namespace LojaBenner.Dtos
{
    public class ProdutoCreateDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Codigo { get; set;} = string.Empty;

        public decimal Valor { get; set; } 

    }
}
