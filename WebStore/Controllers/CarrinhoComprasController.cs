using DLL.BLL.Services.Compras;
using DLL.BLL.ViewModels;
using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Proc = DLL.DAL.Repository.Database.Procedures.ProceduresSQL;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    public class CarrinhoComprasController : Controller
    {
        //private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CarrinhoCompras _carrinhoCompras;


        public CarrinhoComprasController(IUnitOfWork unitOfWork, CarrinhoCompras carrinhoCompras)
        {
            _unitOfWork = unitOfWork;
            //_produtoRepository = produtoRepository;
            _carrinhoCompras = carrinhoCompras;
            _unitOfWork.ProdutoRepository.SetUnitOfWork(_unitOfWork);
        }

        public IActionResult Index()
        {
            _carrinhoCompras.ItensCarrinho = _carrinhoCompras.TodosItemsCarrinho();

            var carrinhoComprasViewModel = new CarrinhoComprasViewModel()
            {
                CarrinhoCompras = _carrinhoCompras,
                TotalCompra = _carrinhoCompras.TotalCompra()
            };

            return View(carrinhoComprasViewModel);
        }


        public RedirectToActionResult AdicionarNoCarrinho(int Id)
        {
            var produtoSelecionado = _unitOfWork.ProdutoRepository.ObterId(Proc.Produto, new { ID = Id });

            if (produtoSelecionado != null)
            {
                _carrinhoCompras.AdicionarCarrinho(produtoSelecionado, 1);
            }
            return RedirectToAction(nameof(Index));

        }


        public RedirectToActionResult RemoverNoCarrinho(int Id)
        {
            var produtoSelecionado = _unitOfWork.ProdutoRepository.ObterId(Proc.Produto, new { ID = Id });

            if (produtoSelecionado != null)
            {
                _carrinhoCompras.RemoverCarrinho(produtoSelecionado);
            }
            return RedirectToAction(nameof(Index));
        }

        public RedirectToActionResult LimparCarrinho()
        {

            _carrinhoCompras.LimparCarrinho();
            return RedirectToAction(nameof(Index));
        }

    }
}
