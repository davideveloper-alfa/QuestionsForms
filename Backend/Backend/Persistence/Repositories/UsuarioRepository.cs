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
    public class UsuarioRepository : IUsuarioRespository
    {

        //Inyeccion de dependencias por contexto
        private readonly AplicationDbContext _context;
        public UsuarioRepository (AplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveUser(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateExistence(Usuario usuario)
        {
            var validateExist = await _context.Usuarios.AnyAsync(x => x.UserName == usuario.UserName);
            return validateExist;
        } 

        public async Task<Usuario> ValidatePassword(int IdUsuario, string PasswordAnterior)
        {
            var usuario = await _context.Usuarios.Where(x => x.Id == IdUsuario && x.Password == PasswordAnterior).FirstOrDefaultAsync();
            return usuario;
        }

        //implementacion de metodo definido enla interfaz para actualizar la contraseña sobre base de datos
        public async Task UpdatePassword(Usuario usuario)
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
