
using Models;
using ProdutoEstoque.Servicos;

async Task<List<Produto>> TodosProdutos()
{
    return await produtoServico.Persistencia.Todos();
}

async Task<List<Estoque>> TodosProdutos()
{
    return await estoqueServico.Persistencia.Todos();
}

while (true)
{
    Console.Clear();

    Console.WriteLine("""
        ======================[Seja Bem vindo ao Estoque Tech]======================

        O que você deseja fazer?
        1 - Cadastrar o Produto
        2 - ver cadastro e estoque de Produto
        3 - Adicionar estoque
        4 - Retirar estoque
        5 - Sair do sistema
        """);

    var opcao = Console.ReadLine()?.Trim();
    Console.Clear();
    bool sair = false;

    switch (opcao)
    {
        case "1":
            Console.Clear();
            await cadastrarProduto();
            break;
        case "2":
            Console.Clear();
            await mostrarEstoque();
            break;
        case "3":
            Console.Clear();
            await adicionandoEstoque();
            break;
        case "4":
            Console.Clear();
            await retirandoEstoque();
            break;
        case "5":
            sair = true;
            break;
        default:
            Console.WriteLine("Opção Inválida");
            break;
    }
    if (sair) break;
}

async Task mostrarEstoque()
{
    Console.Clear();

    var produtos = await TodosProdutos();
    var dadosNoEstoque = await TodosProdutos();
    if(produtos.Count == 0 || dadosNoEstoque.Count == 0)
    {
        mensagem("Não existe produtos ou não existe movimentçãoes de estoque, cadastre um produto");
        return;
    }

    var produto = await capturaProduto();

    var estoqueProduto = await estoqueProduto.ExtratoProduto(produto.Id);
    Console.Clear();
    Console.WriteLine("-------------------------");
    foreach(var estoque in estoqueProduto)
    {
        Console.WriteLine("Data: " + estoque.Data.ToString("dd/MM/yyyy HH:mm:ss");
        Console.WriteLine("Quantidade: " + estoque.Quantidade);
        Console.WriteLine("-------------------------");
    }

    Console.WriteLine($"""
        A quantidade total do estoque do produto é {produto.Nome} é de:
        Qtd - {await estoqueServico.SaldoProduto(produto.Id, estoqueProduto)}
        """);

    Console.WriteLine("Digite enter para continuar");
    Console.ReadLine();
}





