namespace ProgramaEstoque.Models
{
    public class PedidoModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public double ValorProduto { get; set; }
        public double ValorProdutoTotal { get; set; }
    }
}
