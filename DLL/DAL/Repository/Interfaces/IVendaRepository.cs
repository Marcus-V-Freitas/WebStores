using DLL.BLL.Models;

namespace DLL.DAL.Repository.Interfaces
{
    public interface IVendaRepository : IRepository<Venda>
    {
        void CriarVenda(Venda venda);
    }
}
