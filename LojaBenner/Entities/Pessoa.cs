using System.ComponentModel.DataAnnotations;
using LojaBenner.Attribute;

namespace LojaBenner.Entities;
public class Pessoa
{
    public int Id { get; set; }                                 
    [Required, StringLength(120)] public string Nome { get; set; } = null!;
    [Required, Cpf] public string Cpf { get; set; } = null!;
    public string? Endereco { get; set; }
}
