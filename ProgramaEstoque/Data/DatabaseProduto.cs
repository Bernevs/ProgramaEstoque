using System;
using System.Data;
using System.Data.SQLite;
using ProgramaEstoque.Models;

namespace ProgramaEstoque.Data
{
    public class DatabaseProduto : DatabaseManager
    {

        public static void CreateProdutoTable()
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS produto (
                        cd_produto INTEGER PRIMARY KEY AUTOINCREMENT,
                        nome TEXT,
                        preco DOUBLE,
                        quantidade INTEGER
                    )";
                cmd.ExecuteNonQuery();
            }
        }

        public static void AddProduto(string nome, double preco, int quantidade)
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    "INSERT INTO produto (nome, preco, quantidade) VALUES (@nome, @preco, @quantidade)";

                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@preco", preco);
                cmd.Parameters.AddWithValue("@quantidade", quantidade);

                cmd.ExecuteNonQuery();

                CloseConnection();
            }
        }

        public static void AlterarProduto(int cd_produto, string novo_nome, double novo_preco, int nova_quantidade)
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    "UPDATE produto SET nome = @nome, preco = @preco, quantidade = @quantidade WHERE cd_produto = @cd_produto";

                cmd.Parameters.AddWithValue("@cd_produto", cd_produto);
                cmd.Parameters.AddWithValue("@nome", novo_nome);
                cmd.Parameters.AddWithValue("@preco", novo_preco);
                cmd.Parameters.AddWithValue("@quantidade", nova_quantidade);

                cmd.ExecuteNonQuery();

                CloseConnection();
            }
        }

        public static void RemoverProduto(int cd_produto)
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM produto WHERE cd_produto = @cd_produto";
                cmd.Parameters.AddWithValue("@cd_produto", cd_produto);

                cmd.ExecuteNonQuery();

                CloseConnection();
            }
        }

        public static List<ProdutoModel> GetProdutos()
        {
            List<ProdutoModel> produtos = new List<ProdutoModel>();

            using (var conn = GetConnection())
            using (var cmd = new SQLiteCommand("SELECT cd_produto, nome, round(preco, 2), quantidade FROM produto ORDER BY nome", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ProdutoModel produto = new ProdutoModel
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Preco = reader.GetDouble(2),
                        Quantidade = reader.GetInt32(3)
                    };

                    produtos.Add(produto);
                }

                CloseConnection();
            }

            return produtos;
        }

        public static ProdutoModel GetProdutoUnico(int cd_produto)
        {
            ProdutoModel produto = null; // Inicialize como null para o caso de nenhum resultado ser encontrado

            using (var conn = GetConnection())
            {
                using (var cmd = new SQLiteCommand($"SELECT cd_produto, nome, round(preco, 2), quantidade FROM produto WHERE cd_produto = @cd_produto", conn))
                {
                    cmd.Parameters.AddWithValue("@cd_produto", cd_produto);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Verifique se há pelo menos uma linha antes de acessar os dados
                        {
                            produto = new ProdutoModel
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Preco = reader.GetDouble(2),
                                Quantidade = reader.GetInt32(3)
                            };
                        }

                        CloseConnection();
                    }
                }
            }

            return produto;
        }

    }

}
