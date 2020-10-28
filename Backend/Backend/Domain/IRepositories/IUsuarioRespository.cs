using Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Domain.IRepositories
{
    /// <summary>
    /// En esta clase de definen las interfaces que pueden
    /// ser utilizadas para las tareas en particular de un usuario
    /// </summary>
    public interface IUsuarioRespository
    {
        //Interfaz inicial declarada para poder
        //guardar un usuario
        Task SaveUser(Usuario usuario);

        //Return bool y es para validar que 
        //el usuario a registrar no exista en la bd
        Task<bool> ValidateExistence(Usuario usuario);

        //Definicion de metodo Validacion de contraseñas para el cambio de password
        Task<Usuario> ValidatePassword(int IdUsuario, string passwordAnterior);

        //Definicion de metodo Repository para actualizar password
        Task UpdatePassword(Usuario usuario);
    }
}
