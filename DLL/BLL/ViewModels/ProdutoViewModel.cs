using DLL.BLL.Models;
using System.Collections.Generic;

namespace DLL.BLL.ViewModels
{
    public class ProdutoViewModel
    {
        public IEnumerable<Produto> Produtos { get; set; }

        public string CategoriaAtual { get; set; }
    }
}
