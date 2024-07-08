using Karapinha.Model;
using Karapinnha.DTO.Marcacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Converters
{
    public static class MarcacaoServicoConverter
    {
        public static MarcacaoServico ToMarcacaoServico (MarcacaoServicoDTO dto)
        {
            return new MarcacaoServico
            {
                FkServico = dto.FkServico,
                FkProfissional = dto.FkProfissional,
                HoraMarcacao = dto.FkHorario,
            };
        }

        public static MarcacaoServicoDTO ToMarcacaoServicoDTO(MarcacaoServico model)
        {
            return new MarcacaoServicoDTO
            {
                FkServico = model.FkServico,
                FkProfissional = model.FkProfissional,
                FkHorario = model.HoraMarcacao,
            };
        }

        public static MarcacaoServicoGetDTO ToMarcacaoServicoGetDTO(MarcacaoServico model)
        {
            return new MarcacaoServicoGetDTO
            {
                Servico = ServicoConverter.ToServicoDTO(model.Service),
                Profissional = ProfissionalConverter.ToProfissionalDTO(model.Profissional),
                Horario = HorarioConverter.ToHorarioDTO(model.Horario),
            };
        }
    }
}
