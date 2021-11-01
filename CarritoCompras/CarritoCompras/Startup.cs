using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarritoCompras.Data;
using CarritoCompras.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CarritoCompras
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<CarritoComprasContext>(options =>
                    options.UseSqlite("filename=BaseDeDatos.db"));

           

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                opciones =>
                {
                    opciones.LoginPath = "/Usuario/Ingresar";
                    opciones.AccessDeniedPath = "/Usuario/AccesoDenegado";
                    opciones.LogoutPath = "/Usuario/Salir";
                    opciones.ReturnUrlParameter = "/Empleado/Details";
                    //opciones.ReturnUrlParameter = "/Home/Index"; FALTA AGREGAR ENRUTAMIENTO A BOTON SALIR 
                }
            );


            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); //si esta linea no se pone, nunca valida que esta logeado


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseCookiePolicy();

        }
    }
}
