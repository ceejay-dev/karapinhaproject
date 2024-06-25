using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Karapinha.Model
{
    [Table("profissionais")]
    public class Profissional
    {
        [Key]
        public int IdProfissional { get; set; }
        [Required]
        public string? NomeProfissional { get; set; }
        [ForeignKey(nameof(Categoria))]
        public int FkCategoria { get; set; }
        [ServiceStack.DataAnnotations.Unique]
        [Required] 
        public string? EmailProfissional { get; set; }
        public string? FotoProfissional { get; set; }
        [ServiceStack.DataAnnotations.Unique]
        [Required]
        public string? BilheteProfissional { get; set; }
        [ServiceStack.DataAnnotations.Unique]
        [Required]
        public string? TelemovelProfissional { get; set; }

        public List<ProfissionalHorario> Horarios { get; set; }
    }
}
