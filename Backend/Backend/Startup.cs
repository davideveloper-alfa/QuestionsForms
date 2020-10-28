using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.IRepositories;
using Backend.Domain.IServices;
using Backend.Persistence.Context;
using Backend.Persistence.Repositories;
using Backend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Backend
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
            //Importar la conexion hacia la base de datos
            services.AddDbContext<AplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionQuestions")));

            //Para poder usar el user repository
            services.AddScoped<IUsuarioRespository, UsuarioRepository>();

            //Para poder utilizar los servicios
            services.AddScoped<IUsuarioService, UsuarioService>();

            //Agregamos los siguientes servicios para la conexion de un usuario LOGIN
            services.AddScoped<ILoginService, LoginService>();

            //Agregamos el siguiente servicio para poder manejar la informacion en el Repository
            services.AddScoped<ILoginRepository, LoginRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
