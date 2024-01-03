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
    }
}
