using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Model
{
    public class ProfissionalHorario
    {
        [Key]
        public int Id { get; set; }
        public int FkProfissional { get; set; }
        public int FkHorario { get; set; }
        [ForeignKey(nameof(FkProfissional))]
        public Profissional ? Profissional { get; set; }
        [ForeignKey(nameof(FkHorario))]
        public Horario ? Horario { get; set; }
    }
}
