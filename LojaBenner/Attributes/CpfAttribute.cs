using System.ComponentModel.DataAnnotations;

namespace LojaBenner.Attribute;

public sealed class CpfAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var cpf = value as string;
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        cpf = new string(cpf.Where(char.IsDigit).ToArray());
        if (cpf.Length != 11 || cpf.Distinct().Count() == 1) return false;

        int Mod(int sum) => (sum * 10) % 11 % 10;

        int d1 = Mod(Enumerable.Range(0, 9).Sum(i => (cpf[i] - '0') * (10 - i)));
        int d2 = Mod(Enumerable.Range(0, 10).Sum(i => (cpf[i] - '0') * (11 - i)));

        return d1 == cpf[9] - '0' && d2 == cpf[10] - '0';
    }
}
