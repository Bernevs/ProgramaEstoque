using Microsoft.AspNetCore.Mvc;

namespace ProgramaEstoque.Controllers
{
    public class VendaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
