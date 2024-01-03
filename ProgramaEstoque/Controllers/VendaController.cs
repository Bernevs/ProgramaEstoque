using Microsoft.AspNetCore.Mvc;
using ProgramaEstoque.Data;
using ProgramaEstoque.Models;

namespace ProgramaEstoque.Controllers
{
    public class VendaController : Controller
    {
        public IActionResult Index()
        {
            List<ClienteModel> clientes = DatabaseCliente.GetClientes();
            List<ProdutoModel> produtos = DatabaseProduto.GetProdutos();

            VendaModel venda = new VendaModel
            {
                Cliente = clientes,
                Produtos = produtos
            };

            return View(venda);
        }

        public IActionResult ConfirmarVenda()
        {
            return View();
        }

        public IActionResult RealizarVenda()
        {
            //Produto
            int IdProduto = Convert.ToInt32(Request.Form["id_produto"]);
            ProdutoModel produto = DatabaseProduto.GetProdutoUnico(IdProduto);
            int Quantidade = Convert.ToInt32(Request.Form["quantidade"]);

            double ValorTotalProduto = produto.Preco * Quantidade;
            int NovaQuantidade = produto.Quantidade - Quantidade;

            //Cliente
            int IdCliente = Convert.ToInt32(Request.Form["id_cliente"]);
            ClienteModel cliente = DatabaseCliente.GetClienteUnico(IdCliente);

            double ValorTotatlCliente = cliente.ValorTotal + ValorTotalProduto;


            //Adicionar Pedido / Atualizar Estoque / Atualizar Valor do Cliente
            DatabasePedido.AdicionarPedido(cliente.Id, produto.Id, produto.Nome, Quantidade, produto.Preco, ValorTotalProduto);
            DatabaseProduto.AlterarProduto(produto.Id, produto.Nome, produto.Preco, NovaQuantidade);
            DatabaseCliente.AlterarCliente(cliente.Id, cliente.Nome, ValorTotatlCliente);
            return RedirectToAction("Index");
        }
    }
}
