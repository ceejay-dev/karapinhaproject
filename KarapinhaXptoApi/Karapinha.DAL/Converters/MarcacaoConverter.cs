using Karapinha.Model;
using Karapinnha.DTO.Marcacao;
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
                Estado = "pendente",
                FkUtilizador = dto.FkUtilizador,
                PrecoMarcacao = dto.PrecoMarcacao,
                DataMarcacao = dto.DataMarcacao,
                Servicos = dto.Servicos.Select(s=> MarcacaoServicoConverter.ToMarcacaoServico(s)).ToList(),
            };
        }

        public static MarcacaoDTO ToMarcacaoDTO(Marcacao model)
        {
            return new MarcacaoDTO
            {
                FkUtilizador = model.FkUtilizador,
                PrecoMarcacao = model.PrecoMarcacao,
                DataMarcacao = model.DataMarcacao,
                Servicos = model.Servicos.Select(s => MarcacaoServicoConverter.ToMarcacaoServicoDTO(s)).ToList(),
            };
        }

        public static MarcacaoGetDTO ToMarcacaoGetDTO(Marcacao model)
        {
            return new MarcacaoGetDTO
            {
                IdMarcacao = model.IdMarcacao,
                Utilizador = UtilizadorConverter.ToUtilizadorDTO(model.Utilizador),
                PrecoMarcacao = model.PrecoMarcacao,
                DataMarcacao = model.DataMarcacao,
                Estado = model.Estado,
                Servicos = model.Servicos.Select(s => MarcacaoServicoConverter.ToMarcacaoServicoDTO(s)).ToList(),
            };
        }
    }
}
