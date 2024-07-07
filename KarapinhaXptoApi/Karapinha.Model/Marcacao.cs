using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Model
{
    [Table("marcacoes")]
    public class Marcacao
    {
        [Key]
        public int IdMarcacao { get; set; }
        public double PrecoMarcacao { get; set; }
        public string? Estado { get; set; }
        public int FkUtilizador { get; set; }
        [ForeignKey(nameof(FkUtilizador))]
        public Utilizador? Utilizador { get; set; }
        public List<MarcacaoServico> Servicos { get; set; }
    }
}
