using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Model
{
    public class Dado
    {
        [Key]
        public int Id { get; set; }
        public int IdMarcacao {  get; set; }
        public double PrecoMarcacao { get; set; }
        public DateOnly DataMarcacao { get; set; }
        public string ? Estado { get; set; }
        public int FkUtilizador { get; set; }
        public string ? NomeUtilizador { get; set; }
        public string ? NomeServico { get; set; }
        public string ? HorarioDescricao { get; set; }
        public string ? NomeProfissional { get; set; }
    }
}
