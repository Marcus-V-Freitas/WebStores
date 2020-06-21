using DLL.BLL.Models;
using DLL.BLL.Services.Autorizacao;
using DLL.BLL.Services.Compras;
using DLL.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    [ClienteAutorizacao]
    public class VendaController : Controller
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly CarrinhoCompras _carrinhoCompras;


        public VendaController(IVendaRepository vendaRepository, CarrinhoCompras carrinhoCompras)
        {
            _vendaRepository = vendaRepository;
            _carrinhoCompras = carrinhoCompras;
        }

        public IActionResult CheckOut()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CheckOut(Venda venda)
        {

            _carrinhoCompras.ItensCarrinho = _carrinhoCompras.TodosItemsCarrinho();

            if (_carrinhoCompras.ItensCarrinho.Count.Equals(0))
            {
                ModelState.AddModelError("", "Carro Vazio");
            }

            if (ModelState.IsValid)
            {
                _vendaRepository.CriarVenda(venda);
                _carrinhoCompras.LimparCarrinho();
                return RedirectToAction(nameof(CheckOutComplete));
            }

            return View(venda);
        }


        public IActionResult CheckOutComplete()
        {

            ViewBag.MensagemObrigado = "Obrigado";

            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
