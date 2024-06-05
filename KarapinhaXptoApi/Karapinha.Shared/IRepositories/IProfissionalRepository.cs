﻿using Karapinha.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IRepositories
{
    public interface IProfissionalRepository
    {
        Task <Profissional> CreateProfissional (Profissional profissional, IFormFile foto);
        Task<Profissional> GetProfissionalById(int id);
        Task<IEnumerable<Profissional>> GetAllProfissionals();
        Task<bool> DeleteProfissional(int id);
        Task UpdateProfissional(Profissional profissional);
    }
}