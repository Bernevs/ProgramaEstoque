using Microsoft.AspNetCore.Mvc;
using ProgramaEstoque.Data;

namespace ProgramaEstoque.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View(DatabaseCliente.GetClientes());
        }
    }
}
