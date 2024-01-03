namespace ProgramaEstoque.Models
{
    public class PagamentoModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public double ValorPago { get; set; }
        public DateTime Data {  get; set; }
    }
}
