using System;

public class Passaporte
{
    public DateTime Validade { get; set; }
    public string Numero { get; set; }

    public Passaporte(string numero, DateTime validade)
    {
        Numero = numero;
        Validade = validade;
    }

    public bool EstaValido() => Validade >= DateTime.Now;
}

public class Pessoa
{
    public string Nome { get; set; }
    public Passaporte? Passaporte { get; private set; }

    public Pessoa(string nome)
    {
        Nome = nome;
        Passaporte = null;
    }

    public void EmitirPassaporte(string numero, DateTime validade)
    {
        if (Passaporte != null)
            throw new InvalidOperationException("Pessoa já possui um passaporte.");

        if (validade < DateTime.Now)
            throw new InvalidOperationException("Validade do passaporte está expirada.");

        Passaporte = new Passaporte(numero, validade);
    }
}

class Program
{
    static void Main()
    {
        Pessoa pessoa = new Pessoa("João");

        // Teste 1: Emitir passaporte válido
        try
        {
            pessoa.EmitirPassaporte("123456", new DateTime(2030, 12, 31));
            Console.WriteLine($"Passaporte emitido: {pessoa.Passaporte.Numero}, válido até {pessoa.Passaporte.Validade:dd/MM/yyyy}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }

        // Teste 2: Tentar emitir outro passaporte
        try
        {
            pessoa.EmitirPassaporte("789101", new DateTime(2035, 12, 31));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }

        // Teste 3: Tentar emitir passaporte com validade passada
        try
        {
            Pessoa outraPessoa = new Pessoa("Maria");
            outraPessoa.EmitirPassaporte("456789", new DateTime(2020, 12, 31));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}