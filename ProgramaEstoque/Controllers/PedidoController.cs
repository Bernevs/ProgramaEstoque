using Microsoft.AspNetCore.Mvc;
using ProgramaEstoque.Data;
using ProgramaEstoque.Models;

namespace ProgramaEstoque.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult Index()
        {
            int cd_cliente = Convert.ToInt32(Request.Form["cd_cliente"]);
            ClienteModel cliente = DatabaseCliente.GetClienteUnico(cd_cliente);
            List<PedidoModel> pedidos = DatabasePedido.GetPedidos(cd_cliente);

            ClientePedidoModel clientePedido = new ClientePedidoModel
            {
                Cliente = cliente,
                Pedidos = pedidos
            };
            return View(clientePedido);
        }

        public IActionResult ConfirmarRemover()
        {
            int cd_cliente = Convert.ToInt32(Request.Form["cd_cliente"]);
            int cd_pedido = Convert.ToInt32(Request.Form["cd_pedido"]);

            Console.WriteLine(cd_cliente);
            Console.WriteLine(cd_pedido);

            return View(DatabasePedido.GetPedidoUnico(cd_cliente, cd_pedido));
        }
    }
}
