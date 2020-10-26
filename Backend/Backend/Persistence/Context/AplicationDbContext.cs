using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Persistence.Context
{
    /// <summary>
    /// Contexto utilizado para conexion hacia base de datos
    /// </summary>
    public class AplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        //Constructor
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
    }
}