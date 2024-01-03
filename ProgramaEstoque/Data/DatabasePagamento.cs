using ProgramaEstoque.Models;
using System.Data.SQLite;
using System.Globalization;

namespace ProgramaEstoque.Data
{
    public class DatabasePagamento : DatabaseManager
    {
        public static List<PagamentoModel> GetPagamentos(int cdCliente)
        {
            List<PagamentoModel> pagamentos = new List<PagamentoModel>();

            using (var conn = GetConnection())
            {
                using (var cmd = new SQLiteCommand("SELECT cd_pagamento, cd_cliente, round(valor_pago, 2), data FROM pagamento WHERE cd_cliente = @cd_cliente", conn))
                {
                    cmd.Parameters.AddWithValue("@cd_cliente", cdCliente);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PagamentoModel pagamento = new PagamentoModel
                            {
                                Id = reader.GetInt32(0),
                                IdCliente = reader.GetInt32(1),
                                ValorPago = reader.GetDouble(2),
                                Data = DateTime.ParseExact(reader.GetString(3), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                            };

                            pagamentos.Add(pagamento);
                        }
                        CloseConnection();
                    }
                }
            }
            return pagamentos;
        }

        public static PagamentoModel GetPagamentoUnico(int idPagamento, int idCliente)
        {
            PagamentoModel pagamento = null;
            using (var conn = GetConnection())
            {
                using (var cmd = new SQLiteCommand("SELECT cd_pagamento, cd_cliente, round(valor_pago, 2), data FROM pagamento WHERE cd_pagamento = @cd_pagamento AND cd_cliente = @cd_cliente", conn))
                {
                    cmd.Parameters.AddWithValue("@cd_pagamento", idPagamento);
                    cmd.Parameters.AddWithValue("@cd_cliente", idCliente);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pagamento = new PagamentoModel
                            {
                                Id = reader.GetInt32(0),
                                IdCliente = reader.GetInt32(1),
                                ValorPago = reader.GetDouble(2),
                                Data = DateTime.ParseExact(reader.GetString(3), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                            };
                        }
                        CloseConnection();
                    }
                }
            }
            return pagamento;
        }

        public static void RealizarPagamento(int cdCliente, double valorPago, DateTime data)
        {
            using (var conn = GetConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "INSERT INTO pagamento (cd_cliente, valor_pago, data) VALUES (@cd_cliente, @valor_pago, @data)";

                    cmd.Parameters.AddWithValue("@cd_cliente", cdCliente);
                    cmd.Parameters.AddWithValue("@valor_pago", valorPago);
                    cmd.Parameters.AddWithValue("@data", data.ToString("dd/MM/yyyy"));

                    cmd.ExecuteNonQuery();
                    CloseConnection();
                }
            }
        }

        public static void RemoverPagamento(int idPagamento)
        {
            using (var conn = GetConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "DELETE FROM pagamento WHERE cd_pagamento = @cd_pagamento;";

                    cmd.Parameters.AddWithValue("@cd_pagamento", idPagamento);
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                }
            }
        }
    }
}
