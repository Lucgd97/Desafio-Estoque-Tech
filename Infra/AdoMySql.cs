using ProdutoEstoque.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ProdutoEstoque.Infra
{
    public class AdoMySql<T> : IPersistencia<T>
    {
        string connectionString = "server=localhost;user=root;database=mydatabase;port=3306;password=mypassword";

        MySqlConnection connection = new MySqlConnection(connectionString);
        public AdoMySql(string localGravacao)
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
