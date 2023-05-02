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
            connectionString = localGravacao;
        }

        

        public string GetLocalGravacao()
        {
            return connectionString;
        }

        //public async Task<T?> BuscarPorId<T>(string id) where T : class, new()
        //{
        //    string query = "SELECT * FROM users WHERE id = @id";
        //    T? objeto = null;

        //    await using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        await using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@id", id);
        //            connection.Open();
        //            using (DbDataReader reader = await command.ExecuteReaderAsync())
        //            {
        //                objeto = Activator.CreateInstance<T>();

        //                if (reader.Read())
        //                {

        //                    foreach (var prop in typeof(T).GetProperties())
        //                    {
        //                        var valor = reader[prop.Name];
        //                        if (valor != DBNull.Value)
        //                        {
        //                            prop.SetValue(objeto, valor);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return objeto;
        //}

        public async Task<T?> BuscarPorId<T>(string id) where T : class, new()
        {
            string query = "SELECT * FROM users WHERE id = @id";
            T? objeto = null;

            await using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await connection.OpenAsync();
                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            objeto = Activator.CreateInstance<T>();
                            foreach (var prop in typeof(T).GetProperties())
                            {
                                if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                                {
                                    object value = reader[prop.Name];
                                    prop.SetValue(objeto, value);
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
            string query = "INSERT INTO users (nome, codigoProduto, fornecedor) VALUES (@nome, @codigoProduto, @fornecedor)";

            await using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", "Mussarela");
                    command.Parameters.AddWithValue("@codigoProduto", "2");
                    command.Parameters.AddWithValue("@fornecedor", "Devito");
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} rows were affected.");
                }
            }
        }

        public async Task<List<T>> Todos()
        {
            string query = "SELECT * FROM users";
            List<T> listaObjetos = new List<T>();

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
                            string produto = reader.GetString("produto");
                            string fornecedor = reader.GetString("fornecedor");

                            T objeto = Activator.CreateInstance<T>();
                            typeof(T).GetProperty("id").SetValue(objeto, id);
                            typeof(T).GetProperty("produto").SetValue(objeto, produto);
                            typeof(T).GetProperty("fornecedor").SetValue(objeto, fornecedor);

                            listaObjetos.Add(objeto);
                        }
                    }
                }
            }

            return listaObjetos;
        }

        //public async Task<List<T>> Todos()
        //{
        //    string query = "SELECT * FROM users";
        //    List<T> listaObjetos = new List<T>();

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            await connection.OpenAsync();
        //            using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    int id = reader.GetInt32("id");
        //                    string name = reader.GetString("name");
        //                    string email = reader.GetString("email");

        //                    T objeto = new T();
        //                    objeto.id = id;
        //                    objeto.name = name;
        //                    objeto.email = email;

        //                    listaObjetos.Add(objeto);
        //                }
        //            }
        //        }
        //    }

        //    return listaObjetos;
        //}

    }
}
