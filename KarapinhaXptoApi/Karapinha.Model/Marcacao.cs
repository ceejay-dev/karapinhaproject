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
        public ICollection<Servico> Servicos { get; set; } = new List<Servico>();
        public DateOnly DataMarcacao { get; set; }
        public TimeOnly HoraMarcacao { get; set; }
        public string? Estado { get; set; }
        public int FkUtilizador { get; set; }
        [ForeignKey(nameof(FkUtilizador))]
        public Utilizador? Utilizador { get; set; }
    }
}
