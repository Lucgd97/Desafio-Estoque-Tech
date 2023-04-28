using MySql.Data.MySqlClient;
using ProdutoEstoque.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        public async Task<T?> BuscarPorId<T>(string id) where T : class, new()
        {
            string query = "SELECT * FROM users WHERE id = @id";
            T? objeto = null;

            await using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        objeto = Activator.CreateInstance<T>();

                        if (reader.Read())
                        {

                            foreach (var prop in typeof(T).GetProperties())
                            {
                                var valor = reader[prop.Name];
                                if (valor != DBNull.Value)
                                {
                                    prop.SetValue(objeto, valor);
                                }
                            }
                        }
                    }
                }
            }

            return objeto;
        }

        public async Task Excluir(T objeto)
        {            
            
            string query = "DELETE FROM users WHERE id = @id";

            await using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await using (MySqlCommand command = new MySqlCommand(query, connection))
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

            await using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await using (MySqlCommand command = new MySqlCommand(query, connection))
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

            await using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await using (MySqlCommand command = new MySqlCommand(query, connection))
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
            T? objeto;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int id = reader.GetInt32("id");
                            string name = reader.GetString("name");
                            string email = reader.GetString("email");
                            Console.WriteLine($"ID: {id}, Name: {name}, Email: {email}");
                        }
                    }
                }
            }
            return objeto;
        }
    }
}
