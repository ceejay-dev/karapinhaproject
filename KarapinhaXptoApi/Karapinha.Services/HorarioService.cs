using Amazon.Lambda.Model;
using Karapinha.DAL.Converters;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Karapinnha.DTO.Horario;
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

        public async Task<HorarioDTO> CreateSchedule(HorarioDTO dto)
        {
            try
            {
                var horarioAdded = HorarioConverter.ToHorarioDTO(await HorarioRepository.CreateSchedule(HorarioConverter.ToHorario(dto)));
                return horarioAdded;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }

        }

        public async Task<IEnumerable<HorarioDTO>> GetAllSchedules()
        {
            try
            {
                var schedules = await HorarioRepository.GetAllSchedules();
                return schedules.Select(HorarioConverter.ToHorarioDTO);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task<HorarioDTO> GetScheduleById(int id)
        {
            try
            {
                var schedule = await HorarioRepository.GetScheduleById(id);
                return HorarioConverter.ToHorarioDTO(schedule);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
    }
}
