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
                EmailProfissional = dto.EmailProfissional,
                TelemovelProfissional = dto.TelemovelProfissional,
                FotoProfissional = dto.FotoProfissional,
                BilheteProfissional = dto.BilheteProfissional,
                FkCategoria = dto.FkCategoria,
            };
        }

        public static ProfissionalDTO ToProfissionalDTO(Profissional model)
        {
            return new ProfissionalDTO
            {
                IdProfissional = model.IdProfissional,
                NomeProfissional = model.NomeProfissional,
                EmailProfissional = model.EmailProfissional,
                TelemovelProfissional = model.TelemovelProfissional,
                FotoProfissional = model.FotoProfissional,
                BilheteProfissional = model.BilheteProfissional,
                FkCategoria = model.FkCategoria,
            };
        }
    }
}
