using Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Domain.IRepositories
{
    public interface ICuestionarioRepository
    {
        Task CreateCuestionario(Cuestionario cuestionario);

        Task<List<Cuestionario>> GetListCuestionarioByUser(int idUsuario);

        Task<Cuestionario> GetCuestionario(int idCuestionario);

        Task<Cuestionario> FindCuestinoario(int idCuestionario, int idUsuario);

        Task DeleteCuestionario(Cuestionario cuestionario);

        Task<List<Cuestionario>> GetListCuestionarios();
    }
}
