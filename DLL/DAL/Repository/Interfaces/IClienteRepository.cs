using DLL.BLL.Models;

namespace DLL.DAL.Repository.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente Login(string email, string senha);
    }
}
