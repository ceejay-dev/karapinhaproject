using Karapinnha.DTO.Utilizador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinnha.DTO.Marcacao
{
    public class MarcacaoGetDTO
    {
        public int IdMarcacao { get; set; }
        public double PrecoMarcacao { get; set; }
        public string ? Estado { get; set; }
        public DateOnly DataMarcacao { get; set; }
        public UtilizadorDTO ? Utilizador { get; set; }
        public List<MarcacaoServicoDTO> ? Servicos { get; set; }
    }
}
