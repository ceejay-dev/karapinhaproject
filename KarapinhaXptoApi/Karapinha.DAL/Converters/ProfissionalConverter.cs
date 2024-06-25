using Karapinha.Model;
using Karapinnha.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Converters
{
    public class ProfissionalConverter
    {
        public static Profissional ToProfissional(ProfissionalDTO dto)
        {
            return new Profissional
            {
                IdProfissional = dto.IdProfissional,
                NomeProfissional = dto.NomeProfissional,
                FkCategoria = dto.FkCategoria,
                EmailProfissional = dto.EmailProfissional,
                FotoProfissional = dto.FotoProfissional,
                BilheteProfissional = dto.BilheteProfissional,
                TelemovelProfissional = dto.TelemovelProfissional,
                Horarios = dto.Horarios.Select(h => new ProfissionalHorario
                {
                    IdHorario = h
                }).ToList()
            };
        }

        public static ProfissionalDTO ToProfissionalDTO(Profissional model)
        {
            return new ProfissionalDTO
            {
                IdProfissional = model.IdProfissional,
                NomeProfissional = model.NomeProfissional,
                FkCategoria = model.FkCategoria,
                EmailProfissional = model.EmailProfissional,
                FotoProfissional = model.FotoProfissional,
                BilheteProfissional = model.BilheteProfissional,
                TelemovelProfissional = model.TelemovelProfissional,
                Horarios = model.Horarios?.Select(h => h.IdHorario).ToList()
            };
        }
    }
}
