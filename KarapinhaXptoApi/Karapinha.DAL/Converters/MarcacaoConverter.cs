using Karapinha.Model;
using Karapinnha.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Converters
{
    public class MarcacaoConverter
    {
        public static Marcacao ToMarcacao(MarcacaoDTO dto)
        {
            return new Marcacao
            {
                IdMarcacao = dto.IdMarcacao,
                DataMarcacao = dto.DataMarcacao,
                Estado = dto.Estado,
                FkUtilizador = dto.FkUtilizador,
                HoraMarcacao = dto.HoraMarcacao,
                PrecoMarcacao = dto.PrecoMarcacao,
            };
        }

        public static MarcacaoDTO ToMarcacaoDTO(Marcacao model)
        {
            return new MarcacaoDTO
            {
                IdMarcacao = model.IdMarcacao,
                DataMarcacao = model.DataMarcacao,
                Estado = model.Estado,
                FkUtilizador = model.FkUtilizador,
                HoraMarcacao = model.HoraMarcacao,
                PrecoMarcacao = model.PrecoMarcacao,
            };
        }
    }
}
