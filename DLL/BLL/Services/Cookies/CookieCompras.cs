using DLL.BLL.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using acao = DLL.BLL.Models.Enums.CrudAcao;

namespace DLL.BLL.Services.Cookies
{
    public class CookieCompras
    {
        private readonly string Key = "Carrinho.Compras";
        private readonly Cookie _cookie;

        public CookieCompras(Cookie cookie)
        {
            _cookie = cookie;
        }

        /*
         * CRUD
         */

        public void Cadastrar(ItemVenda itemVenda)
        {
            List<ItemVenda> Lista;

            if (_cookie.Existe(Key))
            {
                Lista = Consultar();
                var itemLocalizado = Lista.FirstOrDefault(x => x.ProdutoId.Equals(itemVenda.ProdutoId));

                if (itemLocalizado == null)
                {
                    Lista.Add(itemVenda);
                }
            }
            else
            {
                Lista = new List<ItemVenda>();
                Lista.Add(itemVenda);
            }

            Salvar(Lista);
        }


        public List<ItemVenda> Consultar()
        {
            if (_cookie.Existe(Key))
            {
                string valor = _cookie.Consultar(Key);
                return JsonConvert.DeserializeObject<List<ItemVenda>>(valor);
            }
            else
            {
                return new List<ItemVenda>();
            }
        }

        public void Salvar(List<ItemVenda> itemVendas)
        {
            string valor = JsonConvert.SerializeObject(itemVendas, Formatting.None,
                                                       new JsonSerializerSettings()
                                                       {
                                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                       });
            _cookie.Cadastrar(Key, valor);
        }

        public bool Existe(string Key)
        {
            return !_cookie.Existe(Key).Equals(null);
        }

        public void RemoverCookie()
        {
            _cookie.Remover(Key);
        }

        public ItemVenda Atualizar(Produto produto, acao acao)
        {
            var Lista = Consultar();
            var itemLocalizado = Lista.FirstOrDefault(x => x.ProdutoId.Equals(produto.ID));

            if (itemLocalizado != null)
            {
                Lista.Remove(itemLocalizado);

                switch (acao)
                {
                    case acao.Adicionar:
                        itemLocalizado.Quantidade++;
                        break;
                    case acao.Remover:
                        itemLocalizado.Quantidade--;
                        break;
                }
                Lista.Add(itemLocalizado);
                Salvar(Lista);

            }
            return itemLocalizado;

        }

        public void Remover(Produto produto)
        {
            var Lista = Consultar();
            var itemLocalizado = Lista.FirstOrDefault(x => x.ProdutoId.Equals(produto.ID));

            if (itemLocalizado != null)
            {
                Lista.Remove(itemLocalizado);
                Salvar(Lista);
            }
        }


    }
}
