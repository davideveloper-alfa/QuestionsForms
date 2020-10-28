using Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Domain.IServices
{
    /// <summary>
    /// Aqui se declaran los servicios disponibles
    /// que estaran a traves de las interfaces
    /// </summary>
    public interface IUsuarioService
    {
        Task SaveUser(Usuario usuario);

        //Aqui
        Task<bool> ValidateExistence(Usuario usuario);

        //Validacion de contraseñas para el cambio de password
        Task<Usuario> ValidatePassword(int IdUsuario, string passwordAnterior);

        //Definicion de servicio para actualizar password
        Task UpdatePassword(Usuario usuario);
    }
}
