using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.IServices;
using Backend.Domain.Models;
using Backend.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //Inyeccion de dependencias de los servicios que se conectan al repositry
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
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
                    return Ok(new { message = usuario.UserName });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    } 
}
