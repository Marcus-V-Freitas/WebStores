using DLL.BLL.Models;
using DLL.BLL.Services.Compras;
using DLL.BLL.Services.Cookies;
using DLL.BLL.Services.Middleware;
using DLL.BLL.Services.Sessao;
using DLL.DAL.Repository.Contracts;
using DLL.DAL.Repository.Database;
using DLL.DAL.Repository.Interfaces;
using DLL.DAL.Repository.Unit_Of_Work.Contracts;
using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreContext>(x =>
                                                x.UseSqlServer(Configuration.GetConnectionString("SqlServer"),
                                                y => y.MigrationsAssembly(nameof(DLL))));
            services.AddScoped<IVendaRepository, VendaRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IRepository<Categoria>, Repository<Categoria>>();
            services.AddScoped<IRepository<Cliente>, Repository<Cliente>>();
            services.AddScoped<IRepository<ItemVenda>, Repository<ItemVenda>>();
            services.AddScoped<IRepository<Produto>, Repository<Produto>>();
            services.AddScoped<IRepository<Venda>, Repository<Venda>>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddMemoryCache();

            services.AddScoped<Sessao>();
            services.AddScoped<LoginCliente>();
            services.AddScoped<Cookie>();
            services.AddScoped<CookieCompras>();
            services.AddScoped<CarrinhoCompras>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseExceptionHandler("/Error/Error500");
            app.UseHsts();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseSession();
            app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                 name: "area",
                 areaName: "area",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                 );

                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
