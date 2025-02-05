﻿using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Karapinnha.DTO.Profissional;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Repositories
{
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly KarapinhaDbContext DbContext;
        public ProfissionalRepository(KarapinhaDbContext context)
        {
            DbContext = context;
        }

        public async Task<Profissional> CreateProfissional(Profissional profissional)
        {
            try
            {
                var employee = await DbContext.AddAsync(profissional);
                await DbContext.SaveChangesAsync();
                return employee.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<Profissional> GetProfissionalById(int id)
        {
            return await DbContext.Profissionais.FindAsync(id);
        }

        public async Task<Profissional> GetProfissionalByIdCategoria(int idCategoria)
        {
            return await DbContext.Profissionais
                .Where(p => p.Estado == true && p.FkCategoria == idCategoria)
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Profissional>> GetAllProfissionals()
        {
            return await DbContext.Profissionais
                .Where(p => p.Estado == true)
                .Include(p => p.Horarios)
                .ThenInclude(ph => ph.Horario)
                .ToListAsync();
        }

        public async Task UpdateProfissional(Profissional profissional)
        {
            DbContext.Profissionais.Update(profissional);
            await DbContext.SaveChangesAsync();
        }

        public IEnumerable<dynamic> GetAllProfissionaisWithCategoria()
        {
            var result = from profissional in DbContext.Profissionais
                         join categoria in DbContext.Categorias
                         on profissional.FkCategoria equals categoria.IdCategoria
                         where profissional.Estado == true
                         select new
                         {
                             IdProfissional = profissional.IdProfissional,
                             NomeProfissional = profissional.NomeProfissional,
                             EmailProfissional = profissional.EmailProfissional,
                             NomeCategoria = categoria.NomeCategoria,
                             FkCategoria = profissional.FkCategoria,
                             TelemovelProfissional = profissional.TelemovelProfissional,
                             FotoProfissional = profissional.FotoProfissional
                         };

            return result.ToList();
        }


        public async Task<IEnumerable<dynamic>> GetAllProfissionaisByIdCategoria(int idCategoria)
        {
            var resultado = await (from p in DbContext.Profissionais
                                   join c in DbContext.Categorias on p.FkCategoria equals c.IdCategoria
                                   where (p.FkCategoria == idCategoria && p.Estado==true)
                                   select new
                                   {
                                       IdProfissional = p.IdProfissional,
                                       NomeProfissional = p.NomeProfissional,
                                       FkCategoria = p.FkCategoria,
                                       EmailProfissional = p.EmailProfissional,
                                       FotoProfissional = p.FotoProfissional,
                                       BilheteProfissional = p.BilheteProfissional,
                                       TelemovelProfissional = p.TelemovelProfissional,
                                       Horarios = p.Horarios.Select(h => new
                                       {
                                            IdHorario = h.Horario.IdHorario,
                                            Descricao = h.Horario.Descricao,
                                       }).ToList()
                                   }).ToListAsync();
            return resultado;
        }

        public async Task<IEnumerable<Profissional>> GetMostRequestedProfessionals()
        {
            var mostRequestedProfessionals = await DbContext.MarcacaoServicos
                .GroupBy(ms => ms.FkProfissional)
                .Select(g => new
                {
                    FkProfissional = g.Key,
                    TotalSolicitacoes = g.Count()
                })
                .ToListAsync();

            var professionalsWithCounts = mostRequestedProfessionals
                .Join(
                    DbContext.Profissionais,
                    ms => ms.FkProfissional,
            p => p.IdProfissional,
                    (ms, p) => new Profissional
                    {
                        NomeProfissional = p.NomeProfissional,
                        Contator = ms.TotalSolicitacoes
                    }
                )
                .OrderByDescending(x => x.Contator)
                .ToList();

            return professionalsWithCounts;
        }


    }

}
