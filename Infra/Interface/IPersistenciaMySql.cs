using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoEstoque.Infra.Interface
{
    public interface IPersistenciaMySql<T>
    {
        Task Salvar(T objeto);
        Task ExcluirTudo();
        Task Excluir(T objeto);
        Task<List<T>> Todos();
        Task<T?> BuscarPorId<T>(string id) where T : class, new();


        string GetLocalGravacao();
    }
}
