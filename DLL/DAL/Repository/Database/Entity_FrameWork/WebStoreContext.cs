using DLL.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.DAL.Repository.Database
{
    public class WebStoreContext : DbContext
    {
        public WebStoreContext(DbContextOptions<WebStoreContext> options) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<ItemVenda> ItemsVenda { get; set; }

        public DbSet<Venda> Vendas { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
