﻿using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Repositories
{
    public class HorarioRepository : IHorarioRepository
    {
        private readonly KarapinhaDbContext DbContext;

        public HorarioRepository(KarapinhaDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task <Horario> CreateSchedule (Horario horario)
        {
            var schedule = await DbContext.AddAsync(horario);
            Console.WriteLine(horario.ToString());
            await DbContext.SaveChangesAsync();
            return schedule.Entity;
        }

        public async Task <IEnumerable<Horario>> GetAllSchedules()
        {
            return await DbContext.Horarios.ToListAsync();
        }
    }
}