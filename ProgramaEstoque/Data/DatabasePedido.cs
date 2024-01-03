using ProgramaEstoque.Models;

using System.Data.SQLite;

namespace ProgramaEstoque.Data
{
    public class DatabasePedido : DatabaseManager
    {

        public static void CreatePedidoTable(int cd_cliente)
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    @$"CREATE TABLE IF NOT EXISTS pedido_cliente_{cd_cliente} (
                        cd_pedido INTEGER PRIMARY KEY AUTOINCREMENT,
                        cd_cliente INTEGER,
                        cd_produto INTEGER,
                        nome_produto TEXT,
                        quantidade INTEGER,
                        valor DOUBLE,
                        valor_total_produto DOUBLE,
                        FOREIGN KEY (cd_cliente) REFERENCES cliente(cd_cliente),
                        FOREIGN KEY (cd_produto) REFERENCES produto(cd_produto)
                    )";
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        public static List<PedidoModel> GetPedidos(int cd_cliente)
        {
            List<PedidoModel> pedidos = new List<PedidoModel>();

            using(var conn = GetConnection())
            using(var cmd = new SQLiteCommand($"SELECT cd_pedido, cd_cliente, cd_produto, nome_produto, quantidade, round(valor, 2), round(valor_total_produto, 2) FROM pedido_cliente_{cd_cliente} WHERE cd_cliente = @cd_cliente;", conn))
            {
                cmd.Parameters.AddWithValue("@cd_cliente", cd_cliente);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PedidoModel pedido = new PedidoModel
                        {
                            Id = reader.GetInt32(0),
                            IdCliente = reader.GetInt32(1),
                            IdProduto = reader.GetInt32(2),
                            NomeProduto = reader.GetString(3),
                            Quantidade = reader.GetInt32(4),
                            ValorProduto = reader.GetDouble(5),
                            ValorProdutoTotal = reader.GetDouble(6)
                        };
                        pedidos.Add(pedido);
                    }
                    CloseConnection();
                }
            }
            return pedidos;
        }

        public static PedidoModel GetPedidoUnico(int cd_cliente, int cd_pedido)
        {
            PedidoModel pedido = null;

            using (var conn = GetConnection())
            {
                using (var cmd = new SQLiteCommand($"SELECT cd_pedido, cd_cliente, cd_produto, nome_produto, quantidade, round(valor, 2), round(valor_total_produto, 2) FROM pedido_cliente_{cd_cliente} WHERE cd_pedido = @cd_pedido;", conn))
                {
                    cmd.Parameters.AddWithValue("@cd_pedido", cd_pedido);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pedido = new PedidoModel
                            {
                                Id = reader.GetInt32(0),
                                IdCliente = reader.GetInt32(1),
                                IdProduto = reader.GetInt32(2),
                                NomeProduto = reader.GetString(3),
                                Quantidade = reader.GetInt32(4),
                                ValorProduto = reader.GetDouble(5),
                                ValorProdutoTotal = reader.GetDouble(6)
                            };
                        }
                        CloseConnection();
                    }
                }
            }
            return pedido;
        }

        public static void AdicionarPedido(int idCliente, int idProduto, string nomeProduto, int quantidade, double valorProduto, double valorProdutoTotal)
        {
            using ( var conn = GetConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        $"INSERT INTO pedido_cliente_{idCliente} (cd_cliente, cd_produto, nome_produto, quantidade, valor, valor_total_produto)" +
                        $"VALUES (@cd_cliente, @cd_produto, @nome_produto, @quantidade, @valor, @valor_total_produto);";

                    cmd.Parameters.AddWithValue("@cd_cliente", idCliente);
                    cmd.Parameters.AddWithValue("@cd_produto", idProduto);
                    cmd.Parameters.AddWithValue("@nome_produto", nomeProduto);
                    cmd.Parameters.AddWithValue("@quantidade", quantidade);
                    cmd.Parameters.AddWithValue("@valor", valorProduto);
                    cmd.Parameters.AddWithValue("@valor_total_produto", valorProdutoTotal);

                    cmd.ExecuteNonQuery();
                    CloseConnection();
                }
            }
        }

        public static void RemoverPedido(int cd_pedido, int cd_cliente)
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    $"DELETE FROM pedido_cliente_{cd_cliente} WHERE cd_pedido = @cd_pedido";

                cmd.Parameters.AddWithValue("@cd_pedido", cd_pedido);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }
    }
}
