using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinnha.DTO.Marcacao
{
    public class MarcacaoServicoDTO
    {
        public int FkHorario { get; set; }
        public int FkServico { get; set; }
        public int FkProfissional { get; set; }
    }
}
