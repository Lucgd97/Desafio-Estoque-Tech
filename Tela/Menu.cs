using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tela
{
    class Menu
    {
        public static void Criar()
        {
            int Saida_Programa = 0;
            int Cadastrar_Produto = 1;
            int Cadastrar_Cliente = 2;
            int Estoque = 3;


            while (true)
            {
                string mensagem = "Olá usuário, bem vindo ao programa!\n" +
                    "\n ============ Estoque Tech ============" +
                    "\n Digite uma das opções abaixo:" +
                    "\n 0 - Sair do programa" +
                    "\n 1 - Para cadastrar produto" +
                    "\n 2 - Cadastrar Cliente" +
                    "\n 3 - Para visualizar estoque";
                Console.WriteLine(mensagem);

                int valor = int.Parse(Console.ReadLine());

                if (valor == Saida_Programa)
                {
                    break;
                }
                else if (valor == Cadastrar_Produto)
                {
                    // tela cliente chamar
                    Console.WriteLine("======== Cadastro de Produto ========");
                }
                else if (valor == Cadastrar_Cliente)
                {
                    TelaCliente.Chamar();
                    Console.WriteLine("======== Cadastro de Produto ========");
                }
                else if (valor == Estoque)
                {
                    //tela estoque 
                    Console.WriteLine("========Estoque ========");
                }
                else
                {
                    Console.WriteLine("Opção inválida, digite novamente!");
                }
            }
        }
        
    }
}
