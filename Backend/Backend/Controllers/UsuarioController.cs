using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Domain.IServices;
using Backend.Domain.Models;
using Backend.DTO;
using Backend.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            //inyeccion de dependencias usuario service
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                var validateExist = await _usuarioService.ValidateExistence(usuario);
                if (validateExist)
                {
                    return BadRequest(new { message = "El usuario ya existe " + usuario.UserName });
                }

                //Aqui estamos encriptando el password antes de guardar para que posterior
                //ya vaya la contraseña encriptada
                usuario.Password = Encriptar.EncriptarPassword(usuario.Password);

                //Aqui estamos consumiendo el servicio declarado en la interface
                //que posteriormente ira hacia la interface Repository para poder guardar
                //por medio del contexto
                await _usuarioService.SaveUser(usuario);


                return Ok(new { message = "Usuario registrado con exito." });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex) ;
            }
        }

        [Route("CambiarPassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //con esta linea le indicamos que requiere token para usar
        [HttpPut]
        public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordDTO cambiarPassword)
        {
            try
            {
                //Para obtener los claims del token
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                //aqui obtenemos el token identity, es decir, id del usuario
                var idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);

                //requerimos incialmente encriptar el password anterior para hacer una validacion
                //entre la password registrada anteriormente
                string passwordEncriptado = Encriptar.EncriptarPassword(cambiarPassword.PasswordAnterior);

                //Este servicio recibe estos parametros para validar que la password que se quiere cambiar
                //sea valida con la que esta ya guardada de lo contrario no podria cambiar password
                var usuario = await _usuarioService.ValidatePassword(idUsuario, passwordEncriptado);

                if (usuario == null)
                {
                    return BadRequest(new { message = "Password incorrecto" });
                }
                else
                {
                    //Para actualizar primero la nueva password hay que encriptarla
                    usuario.Password = Encriptar.EncriptarPassword(cambiarPassword.NuevaPassword);

                    //Llamamos al servicio para actualizar password que este se conecta al repository para
                    //conectarse a la base de datos y actualizar
                    await _usuarioService.UpdatePassword(usuario);

                    return Ok(new { message = "la password fue actualizada con exito" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
