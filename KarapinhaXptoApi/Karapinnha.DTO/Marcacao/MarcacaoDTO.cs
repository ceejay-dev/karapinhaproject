using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinnha.DTO.Marcacao
{
    public class MarcacaoDTO
    {
        public double PrecoMarcacao { get; set; }
        public int FkUtilizador { get; set; }
        public DateOnly DataMarcacao { get; set; }
        public List<MarcacaoServicoDTO> Servicos { get; set; }
    }
}
