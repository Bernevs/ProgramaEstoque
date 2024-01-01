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
            using (var cmd = new SQLiteCommand("SELECT cd_cliente, nome, valor_total, valor_pago FROM cliente ORDER BY nome", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ClienteModel cliente = new ClienteModel
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        ValorTotal = reader.GetDouble(2),
                        ValorPago = reader.GetDouble(3)
                    };

                    clientes.Add(cliente);
                }
                CloseConnection();
            }
            return clientes;
        }

        public static void AddCliente(string nome)
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    "INSERT INTO cliente (nome, valor) VALUES (@nome, 0)";

                cmd.Parameters.AddWithValue("@nome", nome);

                cmd.ExecuteNonQuery();

                CloseConnection();
            }
        }
    }
}
