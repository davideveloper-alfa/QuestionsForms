using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.IServices;
using Backend.Domain.Models;
using Backend.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //JWT Configurations
        private readonly IConfiguration _configuration;

        //Inyeccion de dependencias de los servicios que se conectan al repositry
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService, IConfiguration configuration)
        {
            _loginService = loginService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                //Primero tenemos que encriptar la contraseña para verificar contra la base de datos
                usuario.Password = Encriptar.EncriptarPassword(usuario.Password);

                //consumimos el servicio para validar el usuario que intenta hacer login
                var user = await _loginService.ValidateUser(usuario);

                if (user == null)
                {
                    return BadRequest(new { message = "Usuario o contraseña invalidos" });
                }
                else
                {
                    string tokenString = JwtConfigurator.GetToken(user, _configuration);
                    return Ok(new { token = tokenString });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    } 
}
