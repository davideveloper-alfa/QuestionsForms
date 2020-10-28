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
    /// esta clase implementa la interfaz de ILoginServices
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        //Constructor para inyectar el repositorio
        public LoginService(ILoginRepository loginRepository)
        {
            //inyeccion de dependencias
            _loginRepository = loginRepository;
        }

        //implementacion de metodo validate user hacia el repositorio
        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            return await _loginRepository.ValidateUser(usuario);
        }
    }
}
