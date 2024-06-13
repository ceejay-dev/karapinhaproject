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
        public int IdProfissional { get; set; }
        [ForeignKey(nameof(IdProfissional))]
        public Profissional Profissional { get; set; }
        public int IdHorario { get; set; }
        [ForeignKey(nameof(IdHorario))]
        public Horario  Horario { get; set; }
    }
}
