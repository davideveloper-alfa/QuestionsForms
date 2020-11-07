using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.IRepositories;
using Backend.Domain.IServices;
using Backend.Persistence.Context;
using Backend.Persistence.Repositories;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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

            //cors
            //permite conectarse a cualquier app / front end
            services.AddCors(options => options.AddPolicy("AllowWebApp",
                                                           builder => builder.AllowAnyOrigin()
                                                                             .AllowAnyMethod()
                                                                             .AllowAnyHeader()));

            //Agregar autenticacion por jwt
            //Indicamos que es lo que nosotros queremos que valide dentro del objeto tokenValidationParameters
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                       .AddJwtBearer(options =>
                                       options.TokenValidationParameters = new TokenValidationParameters
                                       {
                                           ValidateIssuer = true,
                                           ValidateAudience = true,
                                           ValidateLifetime = true,
                                           ValidateIssuerSigningKey = true,
                                           ValidIssuer = Configuration["Jwt:Issuer"],
                                           ValidAudience = Configuration["Jwt:Audience"],
                                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                                           ClockSkew = TimeSpan.Zero
                                       });


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowWebApp");

            app.UseRouting();

            //Autenticacion por medio de token
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
