using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinnha.DTO
{
    public class ProfissionalDTO
    {
        public int IdProfissional { get; set; }
        public string? NomeProfissional { get; set; }
        public int FkCategoria { get; set; }
        public string? EmailProfissional { get; set; }
        public string? FotoProfissional { get; set; }
        public string? BilheteProfissional { get; set; }
        public string? TelemovelProfissional { get; set; }  
        public List<int> Horarios { get; set; }
    }
}
