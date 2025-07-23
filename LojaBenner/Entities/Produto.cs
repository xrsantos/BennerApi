using System.ComponentModel.DataAnnotations;

namespace LojaBenner.Entities;

public class Produto
{
    public int Id { get; set; }
    [Required, StringLength(120)] public string Nome { get; set; } = null!;
    [Required, StringLength(50)] public string Codigo { get; set; } = null!;
    [Required, Range(0.01, 999999)] public decimal Valor { get; set; }
}
