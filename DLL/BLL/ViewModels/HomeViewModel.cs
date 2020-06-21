using DLL.BLL.Models;
using System.Collections.Generic;

namespace DLL.BLL.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Produto> ProdutoAVenda { get; set; }
    }
}
