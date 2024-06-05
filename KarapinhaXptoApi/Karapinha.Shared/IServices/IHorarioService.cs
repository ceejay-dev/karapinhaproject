using Karapinnha.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IServices
{
    public interface IHorarioService
    {
        Task<HorarioDTO> CreateSchedule(HorarioDTO dto);
    }
}
