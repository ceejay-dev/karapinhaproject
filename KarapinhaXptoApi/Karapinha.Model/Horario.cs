using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Model
{
    [Table("horarios")]
    public class Horario
    {
        [Key]
        public int IdHorario { get; set; }
        [Required]
        public string? Descricao { get; set; }
        public string? Estado { get; set; }
    }
}
