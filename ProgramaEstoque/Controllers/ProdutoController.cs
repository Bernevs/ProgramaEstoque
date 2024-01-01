using Microsoft.AspNetCore.Mvc;
using ProgramaEstoque.Data;
using ProgramaEstoque.Models;

namespace ProgramaEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View(DatabaseManager.GetProdutos());
        }

        public IActionResult ConfirmarCadastro()
        {
            ProdutoModel produto = new ProdutoModel
            {
                Nome = Convert.ToString(Request.Form["nome"]),
                Preco = Convert.ToDouble(Request.Form["preco"]),
                Quantidade = Convert.ToInt32(Request.Form["quantidade"])
            };

            Console.WriteLine(produto);

            return View(produto);
        }

        public IActionResult CadastrarProduto()
        {
            string nome = Convert.ToString(Request.Form["nome"]);
            double preco = Convert.ToDouble(Request.Form["preco"]);
            int quantidade = Convert.ToInt32(Request.Form["quantidade"]);

            DatabaseManager.AddProduto(nome, preco, quantidade);

            return RedirectToAction("Index");
        }

        public IActionResult EditarProduto()
        {
            int cd_produto = Convert.ToInt32(Request.Form["cd_produto"]);
            return View(DatabaseManager.GetProdutoUnico(cd_produto));
        }

        public IActionResult ConfirmarAlterar()
        {
            int cd_produto = Convert.ToInt32(Request.Form["cd_produto"]);
            // Supondo que você tenha uma lógica para obter o produto pelo ID do banco de dados
            ProdutoModel produtoExistente = DatabaseManager.GetProdutoUnico(cd_produto);

            // Atualiza as propriedades do produto com os novos valores do formulário
            produtoExistente.Nome = Convert.ToString(Request.Form["novo_nome"]);
            produtoExistente.Preco = Convert.ToDouble(Request.Form["novo_preco"]);
            produtoExistente.Quantidade = Convert.ToInt32(Request.Form["nova_quantidade"]);

            Console.WriteLine(produtoExistente.Nome);

            return View(produtoExistente);
        }

        public IActionResult AlterarProduto()
        {
            int cd_produto = Convert.ToInt32(Request.Form["cd_produto"]);
            string novo_nome = Convert.ToString(Request.Form["novo_nome"]);
            double novo_preco = Convert.ToDouble(Request.Form["novo_preco"]);
            int nova_quantidade = Convert.ToInt32(Request.Form["nova_quantidade"]);

            DatabaseManager.AlterarProduto(cd_produto, novo_nome, novo_preco, nova_quantidade);

            return RedirectToAction("Index");
        }

        public IActionResult confirmarRemover()
        {
            int cd_produto = Convert.ToInt32(Request.Form["cd_produto"]);
            return View(DatabaseManager.GetProdutoUnico(cd_produto));
        }

        public IActionResult RemoverProduto()
        {
            int cd_produto = Convert.ToInt32(Request.Form["cd_produto"]);
            DatabaseManager.RemoverProduto(cd_produto);
            return RedirectToAction("Index");
        }
    }
}
