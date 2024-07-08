using Karapinha.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IRepositories
{
    public interface IHorarioRepository
    {
        Task<Horario> CreateSchedule(Horario horario);
        Task<IEnumerable<Horario>> GetAllSchedules();
        Task<Horario> GetScheduleById(int id);
        Task<IEnumerable<Horario>> GetAllSchedulesByProfissionalId(int profissionalId);
    }
}
