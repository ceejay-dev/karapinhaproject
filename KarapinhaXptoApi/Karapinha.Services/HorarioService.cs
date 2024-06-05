using Amazon.Lambda.Model;
using Karapinha.DAL.Converters;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Karapinnha.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Services
{
    public class HorarioService : IHorarioService
    {
        private readonly IHorarioRepository HorarioRepository;

        public HorarioService(IHorarioRepository horarioRepository)
        {
            HorarioRepository = horarioRepository;
        }

        public async Task <HorarioDTO> CreateSchedule (HorarioDTO dto)
        {
                var horarioAdded = HorarioConverter.ToHorarioDTO(await HorarioRepository.CreateSchedule(HorarioConverter.ToHorario(dto)));
                return horarioAdded;
     
        }
    }
}
