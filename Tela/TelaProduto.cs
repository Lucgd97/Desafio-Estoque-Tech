using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tela
{
    class TelaProduto
    {
        public static void Chamar()
        {
            Console.WriteLine("========== Cadastrar Prduto ==========");

            while (true)
            {
                string mensagem = "Digite uma das opções abaixo: " +
                    "\n 0 - Sair do cadastro" +
                    "\n 1 - Para cadastrar Produtos" +
                    "\n 2 - Para listar Produtos";

                Console.WriteLine(mensagem);

                int valor = int.Parse(Console.ReadLine());

                if (valor == 0)
                {
                    break;
                }
                else if (valor == 1)
                {
                    var produto = new Produto();

                    Console.WriteLine("Digite o nome do Produto: ");
                    produto.Nome = Console.ReadLine();

                    Console.WriteLine("Digite o Codigo:");
                    produto.Codigo = Console.ReadLine();

                    Console.WriteLine("Digite o Quantidade:");
                    produto.Quantidade = Console.ReadLine();

                    produto.Gravar();
                }
                else
                {
                    var produtos = new Produto().Ler();
                    foreach (Produto p in produtos)
                    {
                        Console.WriteLine("=======================");
                        Console.WriteLine("Nome: " + p.Nome);
                        Console.WriteLine("Codigo: " + p.Codigo);
                        Console.WriteLine("Cpf: " + p.Quantidade);
                        Console.WriteLine("=======================");
                    }
                }
            }
        }
    }
}
