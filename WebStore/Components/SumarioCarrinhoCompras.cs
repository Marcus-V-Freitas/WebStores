using DLL.BLL.Services.Compras;
using DLL.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Components
{
    public class SumarioCarrinhoCompras : ViewComponent
    {
        private readonly CarrinhoCompras _carrinhoCompras;

        public SumarioCarrinhoCompras(CarrinhoCompras carrinhoCompras)
        {
            _carrinhoCompras = carrinhoCompras;
        }

        public IViewComponentResult Invoke()
        {
            _carrinhoCompras.ItensCarrinho = _carrinhoCompras.ItensCarrinho;

            var shoppingCarViewModel = new CarrinhoComprasViewModel
            {
                CarrinhoCompras = _carrinhoCompras,
                TotalCompra = _carrinhoCompras.TotalCompra()

            };
            return View(shoppingCarViewModel);

        }


    }
}
