using ProdutoEstoque.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoEstoque.Infra
{
    public class MySqlDriver<T> : IPersistencia<T>
    {

        public MySqlDriver(string localGravacao)
        {
            this.localGravacao = localGravacao;
        }

        private string localGravacao = "";

        public string GetLocalGravacao()
        {
            return this.localGravacao;
        }

        public Task<T?> BuscarPorId(string id)
        {
            throw new NotImplementedException();
        }        

        public Task Excluir(T objeto)
        {
            throw new NotImplementedException();
        }

        public Task ExcluirTudo()
        {
            throw new NotImplementedException();
        }

        public Task Salvar(T objeto)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> Todos()
        {
            throw new NotImplementedException();
        }
    }
}
