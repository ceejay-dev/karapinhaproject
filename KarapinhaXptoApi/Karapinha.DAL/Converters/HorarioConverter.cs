using Karapinha.Model;
using Karapinnha.DTO.Horario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Converters
{
    public class HorarioConverter
    {

        public static Horario ToHorario (HorarioDTO dto)
        {
            return new Horario
            {
                IdHorario = dto.IdHorario,
                Descricao = dto.Descricao
            };
        }

        public static HorarioDTO ToHorarioDTO(Horario horario)
        {
            return new HorarioDTO
            {
                IdHorario = horario.IdHorario,
                Descricao = horario.Descricao
            };
        }
    }
}
