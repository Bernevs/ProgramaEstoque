namespace ProgramaEstoque.Models
{
    public class ClientePedidoModel
    {
        public ClienteModel Cliente { get; set; }
        public List<PedidoModel> Pedidos { get; set; }
    }
}
