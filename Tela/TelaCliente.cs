using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tela
{
    class TelaCliente
    {
        public static void Chamar()
        {
            Console.WriteLine("========== Cadastrar Cliente ==========");

            while (true)
            {
                string mensagem = "Digite uma das opções abaixo: " +
                    "\n 0 - Sair do cadastro" +
                    "\n 1 - Para cadastrar Clientes" +
                    "\n 2 - Para listar clientes";

                Console.WriteLine(mensagem);

                int valor = int.Parse(Console.ReadLine());

                if (valor == 0)
                {
                    break;
                }
                else if (valor == 1)
                {
                    var cliente = new Cliente();

                    Console.WriteLine("Digite o nome do cliente: ");
                    cliente.Nome = Console.ReadLine();

                    Console.WriteLine("Digite o telefone:");
                    cliente.Telefone = Console.ReadLine();

                    Console.WriteLine("Digite o cpf:");
                    cliente.Cpf = Console.ReadLine();

                    cliente.Gravar();
                }
                else
                {
                    var clientes = new Cliente().Ler();
                    foreach (Cliente c in clientes)
                    {
                        Console.WriteLine("=======================");
                        Console.WriteLine("Nome: " + c.Nome);
                        Console.WriteLine("Telefone: " + c.Telefone);
                        Console.WriteLine("Cpf: " + c.Cpf);
                        Console.WriteLine("=======================");
                    }
                }
            }
        }
    }
}
