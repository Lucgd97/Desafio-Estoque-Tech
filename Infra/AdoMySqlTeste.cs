using MySql.Data.MySqlClient;
using ProdutoEstoque.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProdutoEstoque.Infra
{
    public class AdoMySqlTeste<T> : IPersistencia<T>
    {
        string connectionString = "server=localhost;user id=root;password=33426110;database=estoque_tech_driver";

        public AdoMySqlTeste(string localGravacao)
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
            string query = "SELECT * FROM users WHERE id = @id";

            await using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", 1);
                    connection.Open();
                    await using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string id = reader.GetString("id");
                            string nome = reader.GetString("nome");
                            string codigoProduto = reader.GetString("codigoProduto");
                            string quantidade = reader.GetString("quantidade");
                            Console.WriteLine($"ID: {id}, Nome: {nome}, Código Produto: {codigoProduto}, Quantidade: {quantidade}");
                        }
                        else
                        {
                            Console.WriteLine("No rows were returned.");
                        }
                    }
                }
            }

        }

        public async Task Excluir(T objeto)
        {            
            string connectionString = "server=localhost;user id=root;password=yourpassword;database=yourdatabase";
            string query = "DELETE FROM users WHERE id = @id";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", 1);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} rows were affected.");
                }
            }
        }

        public async Task ExcluirTudo()
        {
            string connectionString = "server=localhost;user id=root;password=yourpassword;database=yourdatabase";
            string query = "DELETE FROM users";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} rows were affected.");
                }
            }
        }

        public async Task Salvar(T objeto)
        {
            string query = "INSERT INTO users (nome, codigoProduto, quantidade) VALUES (@nome, @codigoProduto, @quantidade)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", "Mussarela");
                    command.Parameters.AddWithValue("@codigoProduto", "2");
                    command.Parameters.AddWithValue("@quantidade", "15");
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} rows were affected.");
                }
            }
        }

        public async Task<List<T>> Todos()
        {
            string query = "SELECT * FROM users";

            await using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
        }
    }
}
