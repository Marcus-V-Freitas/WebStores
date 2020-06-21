using DLL.BLL.Models;
using DLL.DAL.Repository.Database;
using DLL.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace DLL.DAL.Repository.Contracts
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        private readonly WebStoreContext _context;

        public ClienteRepository(IConfiguration configuration, WebStoreContext context) : base(configuration)
        {
            _context = context;
        }

        public Cliente Login(string email, string senha)
        {
            return _context.Clientes.AsNoTracking().FirstOrDefault(x => x.Email.Equals(email) && x.Senha.Equals(senha));
        }
    }
}
