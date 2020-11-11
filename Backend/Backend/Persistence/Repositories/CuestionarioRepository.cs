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
    public class CuestionarioRepository : ICuestionarioRepository
    {
        private readonly AplicationDbContext _context;

        public CuestionarioRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateCuestionario(Cuestionario cuestionario)
        {
            _context.Add(cuestionario);
            await _context.SaveChangesAsync();
        }

        // aqui obtenemos todos los cuestionarios activos del usuario logeado
        public async Task<List<Cuestionario>> GetListCuestionarioByUser(int idUsuario)
        {
            var listCuestionario = await _context.Cuestionario.Where(x => x.Activo == 1 && x.UsuarioId == idUsuario).ToListAsync();
            return listCuestionario;
        }

        public async Task<Cuestionario> GetCuestionario(int idCuestionario)
        {
            var cuestionario = await _context.Cuestionario.Where(x => x.Id == idCuestionario && x.Activo == 1)
                                                          .Include(x => x.listPreguntas)
                                                          .ThenInclude(x => x.listRespuesta)
                                                          .FirstOrDefaultAsync();

            return cuestionario;
        }

        public async Task<Cuestionario> FindCuestinoario(int idCuestionario, int idUsuario)
        {
            var cuestionario = await _context.Cuestionario.Where(x => x.Id == idCuestionario && x.Activo == 1 && x.UsuarioId == idUsuario).FirstOrDefaultAsync();
            return cuestionario;
        }

        public async Task DeleteCuestionario(Cuestionario cuestionario)
        {
            cuestionario.Activo = 0;
            _context.Entry(cuestionario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cuestionario>> GetListCuestionarios()
        {
            var listCuestionarios = await _context.Cuestionario.Where(x => x.Activo == 1)
                                                               .Select(o => new Cuestionario
                                                               {
                                                                   Id = o.Id,
                                                                   Nombre = o.Nombre,
                                                                   Descripcion = o.Descripcion,
                                                                   FechaCreacion = o.FechaCreacion,
                                                                   Usuarios = new Usuario { UserName = o.Usuarios.UserName}
                                                               }).ToListAsync();
            return listCuestionarios;
        }
    }
}
