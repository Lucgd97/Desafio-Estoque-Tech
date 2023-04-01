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

        public async Task<T?> BuscarPorId(string id)
        {
            throw new NotImplementedException();
        }        

        public async Task Excluir(T objeto)
        {
            throw new NotImplementedException();
        }

        public async Task ExcluirTudo()
        {
            throw new NotImplementedException();
        }

        public async Task Salvar(T objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> Todos()
        {
            throw new NotImplementedException();
        }
    }
}
