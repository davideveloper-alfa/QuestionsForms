﻿using Backend.Domain.IRepositories;
using Backend.Domain.IServices;
using Backend.Domain.Models;
using Backend.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class CuestionarioService: ICuestionarioService
    {
        private readonly ICuestionarioRepository _cuestionarioRepository;

        public CuestionarioService(ICuestionarioRepository cuestionarioRepository)
        {
            _cuestionarioRepository = cuestionarioRepository;
        }

        public async Task CreateCuestionario(Cuestionario cuestionario)
        {
            await _cuestionarioRepository.CreateCuestionario(cuestionario);
        }

        public async Task<List<Cuestionario>> GetListCuestionarioByUser(int idUsuario)
        {
            return await _cuestionarioRepository.GetListCuestionarioByUser(idUsuario);
        }

        public async Task<Cuestionario> GetCuestionario(int idCuestionario)
        {
            return await _cuestionarioRepository.GetCuestionario(idCuestionario);
        }

        public async Task<Cuestionario> FindCuestinoario(int idCuestionario, int idUsuario)
        {
            return await _cuestionarioRepository.FindCuestinoario(idCuestionario, idUsuario);
        }

        public async Task DeleteCuestionario(Cuestionario cuestionario)
        {
            await _cuestionarioRepository.DeleteCuestionario(cuestionario);
        }

        public async Task<List<Cuestionario>> GetListCuestionarios()
        {
            return await _cuestionarioRepository.GetListCuestionarios();
        }
    }
}
