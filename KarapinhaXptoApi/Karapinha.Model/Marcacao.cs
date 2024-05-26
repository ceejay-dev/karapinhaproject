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
        public DateTime DataMarcacao { get; set; }
        public DateTime HoraMarcacao { get; set; }
        public string? Estado { get; set; }
        public int FkUsuario { get; set; }
        [ForeignKey(nameof(FkUsuario))]
        public Usuario? Usuario { get; set; }
    }
}
