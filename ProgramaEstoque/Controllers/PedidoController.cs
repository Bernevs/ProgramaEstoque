using Microsoft.AspNetCore.Mvc;
using ProgramaEstoque.Data;
using ProgramaEstoque.Models;

namespace ProgramaEstoque.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult Index(int cd_cliente)
        {
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


            return View(DatabasePedido.GetPedidoUnico(cd_cliente, cd_pedido));
        }

        public IActionResult RemoverPedido()
        {
            int cd_pedido = Convert.ToInt32(Request.Form["cd_pedido"]);
            int cd_cliente = Convert.ToInt32(Request.Form["cd_cliente"]);

            ClienteModel cliente = DatabaseCliente.GetClienteUnico(cd_cliente);
            PedidoModel pedido = DatabasePedido.GetPedidoUnico(cd_cliente, cd_pedido);
            ProdutoModel produto = DatabaseProduto.GetProdutoUnico(pedido.IdProduto);

            //Atualizando o valor do cliente
            double ValorTotal = cliente.ValorTotal - pedido.ValorProdutoTotal;
            DatabaseCliente.AlterarCliente(cd_cliente, cliente.Nome, ValorTotal);

            //Atualizando o Estoque
            int quantidade = produto.Quantidade + pedido.Quantidade;
            DatabaseProduto.AlterarProduto(pedido.IdProduto, pedido.NomeProduto, pedido.ValorProduto, quantidade);

            //Remover o pedido
            DatabasePedido.RemoverPedido(cd_pedido, cd_cliente);
            return RedirectToAction("Index", new {cd_cliente = cd_cliente});
        }
    }
}
