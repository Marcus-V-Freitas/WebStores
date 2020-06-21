using DLL.BLL.Models;
using DLL.BLL.Services.Cookies;
using DLL.DAL.Repository.Database;
using System.Collections.Generic;
using System.Linq;
using acao = DLL.BLL.Models.Enums.CrudAcao;

namespace DLL.BLL.Services.Compras
{
    public class CarrinhoCompras
    {
        private readonly CookieCompras _cookie;
        private readonly WebStoreContext _context;
        public List<ItemVenda> ItensCarrinho { get; set; }

        public CarrinhoCompras(CookieCompras cookie, WebStoreContext context)
        {
            _cookie = cookie;
            _context = context;
        }

        public void AdicionarCarrinho(Produto produto, int Quantidade)
        {
            if (_cookie.Consultar().FirstOrDefault(x => x.ProdutoId.Equals(produto.ID)) == null)
            {
                var itemVenda = new ItemVenda()
                {
                    ProdutoId = produto.ID,
                    Produto = produto,
                    Quantidade = Quantidade
                };
                _cookie.Cadastrar(itemVenda);

            }
            else
            {
                _cookie.Atualizar(produto, acao.Adicionar);
            }
        }

        public int RemoverCarrinho(Produto produto)
        {
            var valor = _cookie.Atualizar(produto, acao.Remover);

            if (valor.Quantidade.Equals(0))
            {
                _cookie.Remover(produto);
            }

            return valor.Quantidade;
        }

        public List<ItemVenda> TodosItemsCarrinho()
        {
            if (ItensCarrinho == null)
            {
                List<ItemVenda> itemCarrinhoCookie = _cookie.Consultar();
                List<ItemVenda> itemCarrinhoCompleto = new List<ItemVenda>();

                foreach (var item in itemCarrinhoCookie)
                {
                    item.Produto = _context.Produtos.FirstOrDefault(x => x.ID.Equals(item.ProdutoId));
                    itemCarrinhoCompleto.Add(item);
                }
                ItensCarrinho = itemCarrinhoCompleto;
            }

            return ItensCarrinho;
        }

        public decimal TotalCompra()
        {
            return TodosItemsCarrinho().Select(x => x.Produto.Preço * x.Quantidade).Sum();
        }

        public void LimparCarrinho()
        {
            _cookie.RemoverCookie();
        }
    }
}
