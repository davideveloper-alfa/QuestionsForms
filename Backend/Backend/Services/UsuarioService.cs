using Backend.Domain.IRepositories;
using Backend.Domain.IServices;
using Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    /// <summary>
    /// Aqui se implementan las interfaces para este caso que es todo lo relacionado a Usuario
    /// </summary>
    public class UsuarioService: IUsuarioService
    {
        private readonly IUsuarioRespository _usuarioRespository;

        public UsuarioService(IUsuarioRespository usuarioRespository)
        {
            _usuarioRespository = usuarioRespository;
        }

        public async Task SaveUser(Usuario usuario)
        {
            await _usuarioRespository.SaveUser(usuario);
        }

        public async Task<bool> ValidateExistence(Usuario usuario)
        {
            return await _usuarioRespository.ValidateExistence(usuario);
        }

        //Con este metodo implementado recibimos 2 parametros para luego procesarlos hacia la base de datos 
        public async Task<Usuario> ValidatePassword(int IdUsuario, string passwordAnterior)
        {
            return await _usuarioRespository.ValidatePassword(IdUsuario, passwordAnterior);
        }

        //Este metodo y los definidos aqui refieren a la logica de negocio y que 
        //posterior de aqui se conectan al repository donde esta todo lo relacionado al acceso
        // a base de datos, referente a acciones CRUD
        public async Task UpdatePassword(Usuario usuario)
        {
            await _usuarioRespository.UpdatePassword(usuario);
        }
    }
}
