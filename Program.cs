
using Models;
using ProdutoEstoque.Infra;
using ProdutoEstoque.Servicos;

string localGravacaoDev = Environment.GetEnvironmentVariable("LOCAL_GRAVACAO_DEV_ESTOQUE_TECH") ?? "/tmp";
ProdutoServico produtoServico = new ProdutoServico(new JsonDriver<Produto>(localGravacaoDev));
EstoqueServico estoqueServico = new EstoqueServico(new JsonDriver<Estoque>(localGravacaoDev));

async Task<List<Produto>> TodosProdutos()
{
    return await produtoServico.Persistencia.Todos();
}

async Task<List<Estoque>> TodosExtratos()
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

    var estoqueProduto = await estoqueProduto.TodosExtratos(produto.Id);
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

async Task listarProdutosCadastrados()
{
    if((await TodosProdutos()).Count == 0)
    {
        await menuCadastrarProdutosSeNaoExiste();
    }

    await mostrarProdutos(false, 0, "===============[ Selecione um produto da lista]===============");
}

async Task mostrarProdutos(
    bool sleep = true,
    int timerSleep = 2000,
    string header = "===============[ Lista de Produtos ]===============")
{
    Console.Clear();
    Console.WriteLine(header); ;

    foreach (var produto in (await TodosProdutos()))
    {
        Console.WriteLine("Id: " + produto.Id);
        Console.WriteLine("Nome: " + produto.Nome);
        Console.WriteLine("Codigo: " + produto.Codigo);
        Console.WriteLine("Fornecedor: " + produto.Fornecedor);
        Console.WriteLine("-------------------------");

        if (sleep)
        {
            Thread.Sleep(timerSleep);
            Console.Clear();
        }
    }
}

async Task cadastrarProduto()
{
    var id = Guid.NewGuid().ToString();

    Console.WriteLine("Informe o nome do Produto:");
    var nomeProduto = Console.ReadLine();

    Console.WriteLine($"Informe o codigo do produto {nomeProduto}: ");
    var codigo = Console.ReadLine();

    Console.WriteLine($"informe o fornecedor do produto {nomeProduto}: ");
    var fornecedor = Console.ReadLine();

    if ((await TodosProdutos()).Count > 0)
    {
        Produto? pro = (await TodosProdutos()).Find(p => p.Codigo== codigo);
        if (pro != null)
        {
            mensagem($"Produto já cadastrado com este codigo {codigo}, cadastre novamente");
            await cadastrarProduto();
        }
    }

    await produtoServico.Persistencia.Salvar(new Produto
    {
        Id = id,
        Nome = nomeProduto ?? "[Sem Nome]",
        Codigo = codigo != null ? codigo : "[Sem Codigo]",
        Fornecedor = fornecedor ?? "[Sem Fornecedor]"
    });

    mensagem($""" {nomeProduto} cadastrado com sucesso""");
}

void mensagem(string msg)
{
    Console.Clear();
    Console.WriteLine(msg);
    Thread.Sleep(1500);
}

async Task retirandoEstoque()
{
    Console.Clear();
    var produto = await capturaProduto();
    Console.Clear();
    Console.WriteLine("Digite o valor para retirada:");
    int estoqueAtual = Convert.ToInt32(Console.ReadLine());

    await estoqueServico.Persistencia.Salvar(new Estoque
    {
        Id = Guid.NewGuid().ToString(),
        IdProduto = produto.Id,
        Quantidade = estoqueAtual * -1,
        Data = DateTime.Now
    });

    mensagem($"""
        Retirada realizada com sucesso ...
        Saldo do estoque {produto.Nome} é de Qtd: {await estoqueServico.SaldoProduto(produto.Id)}
        """);
}

async Task adicionandoEstoque()
{
    Console.Clear();
    var produto = await capturaProduto();
    Console.Clear();
    Console.WriteLine("Digite o valor para aumentar estoque:");
    int estoqueAtual = Convert.ToInt32(Console.ReadLine());

    await estoqueServico.Persistencia.Salvar(new Estoque
    {
        Id = Guid.NewGuid().ToString(),
        IdProduto = produto.Id,
        Quantidade = estoqueAtual,
        Data = DateTime.Now
    });

    mensagem($"""
        Aumento de estoque adicionado com sucesso ...
        Saldo do estoque {produto.Nome} é de Qtd: {await estoqueServico.SaldoProduto(produto.Id)}
        """);
}

async Task<Produto> capturaProduto()
{
    await listarProdutosCadastrados();
    Console.WriteLine("Digite o ID do produto");
    var idProduto = Console.ReadLine()?.Trim();
    if(string.IsNullOrEmpty(idProduto))
    {
        mensagem("Id do produto inválido!");
        Console.Clear();

        await menuCadastrarProdutosSeNaoExiste();

        return await capturaProduto();
    }
    Produto? produto = await estoqueServico.Persistencia.BuscarPorId(idProduto);

    if(produto == null)
    {
        mensagem("Produto não encontrado na lista, digite o ID corretamente da produtos");
        Console.Clear();

        await menuCadastrarProdutosSeNaoExiste();

        return await capturaProduto();
    }

    return produto;

}

async Task menuCadastrarProdutosSeNaoExiste()
{
    Console.WriteLine("""
        O que deseja fazer?
        1 - Cadastrar Produto
        2 -Voltar ao menu
        3 - Sair do programa
        """);

    var opcao = Console.ReadLine()?.Trim();

    switch(opcao)
    {
        case "1":
            await cadastrarProduto();
            break;
        case "2":
            break;
        case "3":
            System.Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;
    }
}





