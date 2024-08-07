using System.Text.RegularExpressions;

namespace RideApp.Domain.Utilities;

public class Cpf
{
    private const int CpfLength = 11;
    private const int FactorFirstDigit = 10;
    private const int FactorSecondDigit = 11;
    public string Value { get; }
    
    public Cpf(){}
    public Cpf(string cpf)
    {
        Value = Validate(cpf);
    }
    
    private string Validate(string cpf)
    {
        if (string.IsNullOrEmpty(cpf)) throw new ArgumentException("Cpf invalid.");
        string normalizedCpf = RemoveNonDigits(cpf);
        if (normalizedCpf.Length != CpfLength) throw new ArgumentException("Cpf invalid.");
        CheckAllDigitsTheSame(normalizedCpf);
        var digit1 = CalculateDigit(normalizedCpf, FactorFirstDigit);
        var digit2 = CalculateDigit(normalizedCpf, FactorSecondDigit);
        Console.WriteLine(digit1);
        Console.WriteLine(digit2);
        if (!(digit1 == (normalizedCpf[9] - '0') && digit2 == (normalizedCpf[10] - '0'))) throw new ArgumentException("Cpf invalid.");
        return normalizedCpf;
    }

    private static string RemoveNonDigits(string cpf)
    {
        return Regex.Replace(cpf, @"/\D/g", "");
    }

    private static void CheckAllDigitsTheSame(string cpf)
    {
        if (cpf.All(c => c == cpf[0])) throw new ArgumentException("Cpf invalid.");
    }

    private static int CalculateDigit(string cpf, int factor)
    {
        int total = 0;
        foreach (var digit in cpf)
        {
            if (factor > 1)
            {
                total += (digit - '0') * factor;
                factor--;
            }
        }
        var remainder = total % 11;
        return (remainder < 2) ? 0 : 11 - remainder;
    }
    
}