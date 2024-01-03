using Microsoft.AspNetCore.Mvc;
using ProgramaEstoque.Data;

namespace ProgramaEstoque.Controllers
{
    public class PagamentoController : Controller
    {
        public IActionResult Index()
        {
            int IdCliente = Convert.ToInt32(Request.Form["id_cliente"]);
            return View(DatabasePagamento.GetPagamentos(IdCliente));
        }
    }
}
