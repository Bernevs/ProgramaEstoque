using ProgramaEstoque.Models;

using System.Data.SQLite;

namespace ProgramaEstoque.Data
{
    public class DatabasePedido : DatabaseManager
    {
        public static List<PedidoModel> GetPedidos(int cd_cliente)
        {
            List<PedidoModel> pedidos = new List<PedidoModel>();

            using(var conn = GetConnection())
            using(var cmd = new SQLiteCommand($"SELECT cd_pedido, cd_cliente, cd_produto, nome_produto, quantidade, round(valor, 2), round(valor_total_produto, 2) FROM pedido_cliente_{cd_cliente} WHERE cd_cliente = {cd_cliente};", conn))
            using(var reader = cmd.ExecuteReader())
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
            return pedidos;
        }
    }
}
