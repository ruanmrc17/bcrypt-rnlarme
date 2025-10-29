// Arquivo: RNLARME.Tools/Program.cs (ou HashGenerator.cs)

using System;
using BCrypt.Net;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("    RNLARME - FERRAMENTA DE CRIAÇÃO DE HASH    ");
        Console.WriteLine("---------------------------------------------");
        
        // Se houver argumentos, tenta processar diretamente
        if (args.Length > 0)
        {
            string senhaPura = args[0];
            Console.WriteLine($"Processando a senha passada como argumento: '{senhaPura}'");
            GeraHash(senhaPura);
            return; // Encerra após processar o argumento
        }

        // Se não houver argumentos, entra no modo interativo
        ModoInterativo();
    }
    
    private static void ModoInterativo()
    {
        while (true)
        {
            Console.Write("\nDigite a senha que você deseja criptografar (ou 'sair' para fechar): ");
            string senhaPura = Console.ReadLine() ?? "";

            if (senhaPura.Equals("sair", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (string.IsNullOrWhiteSpace(senhaPura))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("A senha não pode ser vazia.");
                Console.ResetColor();
                continue;
            }

            GeraHash(senhaPura);
        }
        Console.WriteLine("\nFerramenta encerrada.");
    }
    
    private static void GeraHash(string senhaPura)
    {
        try
        {
            // Gera o hash usando o BCrypt
            string hashGerado = BCrypt.Net.BCrypt.HashPassword(senhaPura);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ HASH GERADO COM SUCESSO:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(hashGerado);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("COPIE este hash e cole-o no campo 'passwordHash' do seu novo usuário no MongoDB Atlas.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro ao gerar o hash: {ex.Message}");
            Console.ResetColor();
        }
    }
}