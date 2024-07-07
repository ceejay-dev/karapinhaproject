using Karapinnha.DTO.Horario;
using Karapinnha.DTO.Profissional;
using Karapinnha.DTO.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinnha.DTO.Marcacao
{
    public class MarcacaoServicoGetDTO
    {
        public HorarioDTO Horario { get; set; }
        public ServicoDTO Servico { get; set; }
        public ProfissionalDTO Profissional { get; set; }
        public DateTime DataMarcacao { get; set; }
    }
}
