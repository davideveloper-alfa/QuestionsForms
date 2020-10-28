using Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Domain.IRepositories
{
    public interface ILoginRepository
    {
        //Definicion de metodos de repositorio
        Task<Usuario> ValidateUser(Usuario usuario);
    }
}
