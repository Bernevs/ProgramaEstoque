using ProgramaEstoque.Models;
using System.Data.SQLite;

namespace ProgramaEstoque.Data
{
    public class DatabaseCliente : DatabaseManager
    {

        public static List<ClienteModel> GetClientes()
        {
            List<ClienteModel> clientes = new List<ClienteModel>();

            using (var conn = GetConnection())
            using (var cmd = new SQLiteCommand("SELECT cd_cliente, nome, round(valor_total, 2) FROM cliente ORDER BY nome", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ClienteModel cliente = new ClienteModel
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        ValorTotal = reader.GetDouble(2)
                    };

                    clientes.Add(cliente);
                }
                CloseConnection();
            }
            return clientes;
        }

        public static ClienteModel GetClienteUnico(int cd_cliente)
        {
            ClienteModel cliente = null;

            using(var conn = GetConnection())
            using (var cmd = new SQLiteCommand($"SELECT cd_cliente, nome, round(valor_total, 2) FROM cliente WHERE cd_cliente = {cd_cliente}", conn))
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    cliente = new ClienteModel
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        ValorTotal = reader.GetDouble(2)
                    };
                }
                CloseConnection();
            }
            return cliente;
        }

        public static void AddCliente(string nome)
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    "INSERT INTO cliente (nome, valor_total) VALUES (@nome, 0, 0)";

                cmd.Parameters.AddWithValue("@nome", nome);

                cmd.ExecuteNonQuery();

                CloseConnection();
            }
        }

        public static void RemoverCliente(int cd_cliente)
        {
            using(var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    "DELETE FROM cliente WHERE cd_cliente = @cd_cliente";

                cmd.Parameters.AddWithValue("@cd_cliente", cd_cliente);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }
    }
}
