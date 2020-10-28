using Backend.Domain.IRepositories;
using Backend.Domain.Models;
using Backend.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Persistence.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginRepository : ILoginRepository
    {
        //Inyeccion de dependencias del contexto, es decir, base de datos
        private readonly AplicationDbContext _context;

        public LoginRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            var user = await _context.Usuarios.Where(x => x.UserName == usuario.UserName && x.Password == usuario.Password).FirstOrDefaultAsync();
            return user;
        }
    }
}
