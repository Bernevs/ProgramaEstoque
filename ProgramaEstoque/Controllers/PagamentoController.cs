using Microsoft.AspNetCore.Mvc;
using ProgramaEstoque.Data;
using ProgramaEstoque.Models;

namespace ProgramaEstoque.Controllers
{
    public class PagamentoController : Controller
    {
        public IActionResult Index(int IdCliente)
        {
            if (IdCliente == 0)
            {
                IdCliente = Convert.ToInt32(Request.Form["id_cliente"]);
            }

            return View(DatabasePagamento.GetPagamentos(IdCliente));
        }

        public IActionResult Pagamento()
        {
            int IdCliente = Convert.ToInt32(Request.Form["id_cliente"]);
            return View(DatabaseCliente.GetClienteUnico(IdCliente));
        }

        public IActionResult RealizarPagamento()
        {
            int IdCliente = Convert.ToInt32(Request.Form["id_cliente"]);
            ClienteModel cliente = DatabaseCliente.GetClienteUnico(IdCliente);

            double ValorPago = Convert.ToDouble(Request.Form["valor_pago"]);
            DateTime Data = Convert.ToDateTime(Request.Form["data"]);

            Console.WriteLine(IdCliente);

            double Valor = cliente.ValorTotal - ValorPago;

            DatabasePagamento.RealizarPagamento(cliente.Id, ValorPago, Data);
            DatabaseCliente.AlterarCliente(cliente.Id, cliente.Nome, Valor);
            return RedirectToAction("Index", new { IdCliente = IdCliente });
        }

        public IActionResult ConfirmarRemover()
        {
            int IdPagamento = Convert.ToInt32(Request.Form["id_pagamento"]);
            int IdCliente = Convert.ToInt32(Request.Form["id_cliente"]);

            return View(DatabasePagamento.GetPagamentoUnico(IdPagamento, IdCliente));
        }

        public IActionResult RemoverPagamento()
        {
            int IdCliente = Convert.ToInt32(Request.Form["id_cliente"]);
            int IdPagamento = Convert.ToInt32(Request.Form["id_pagamento"]);

            PagamentoModel pagamento = DatabasePagamento.GetPagamentoUnico(IdPagamento, IdCliente);
            ClienteModel cliente = DatabaseCliente.GetClienteUnico(IdCliente);
            double Valor = cliente.ValorTotal + pagamento.ValorPago;

            DatabaseCliente.AlterarCliente(cliente.Id, cliente.Nome, Valor);
            DatabasePagamento.RemoverPagamento(IdPagamento);
            return RedirectToAction("Index", new {IdCliente = IdCliente});
        }
    }
}
