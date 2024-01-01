using Microsoft.AspNetCore.Mvc;
using ProgramaEstoque.Data;
using ProgramaEstoque.Models;

namespace ProgramaEstoque.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View(DatabaseCliente.GetClientes());
        }

        public IActionResult ConfirmarCadastro()
        {
            ClienteModel cliente = new ClienteModel
            {
                Nome = Convert.ToString(Request.Form["nome"]),
                ValorTotal = 0,
                ValorPago = 0
            };

            return View(cliente);
        }

        public IActionResult CadastrarCliente()
        {
            string nome = Convert.ToString(Request.Form["nome"]);
            DatabaseCliente.AddCliente(nome);

            return RedirectToAction("Index");
        }

        public IActionResult ConfirmarRemover()
        {
            int cd_cliente = Convert.ToInt32(Request.Form["cd_cliente"]);
            return View(DatabaseCliente.GetClienteUnico(cd_cliente));
        }

        public IActionResult RemoverCliente()
        {
            int cd_cliente = Convert.ToInt32(Request.Form["cd_cliente"]);

            DatabaseCliente.RemoverCliente(cd_cliente);
            return RedirectToAction("Index");
        }
    }
}
