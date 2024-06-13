using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinnha.DTO
{
    public class MarcacaoDTO
    {
        public int IdMarcacao { get; set; }
        public double PrecoMarcacao { get; set; }
        public DateOnly DataMarcacao { get; set; }
        public int FkHorario {  get; set; }
        public string? Estado { get; set; }
        public int FkUtilizador { get; set; }
    }
}
