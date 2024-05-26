using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Model
{
    [Table("servicos")]
    public class Servico
    {
        [Key]
        public int IdServico { get; set; }
        public string? NomeServico { get; set; }
        public double Preco {  get; set; }

        public int FkCategoria { get; set; }
        [ForeignKey(nameof(FkCategoria))]
        public  Categoria? Categoria { get; set; }
        public int IdMarcacao { get; set; } 
        public Marcacao Marcacao { get; set; } = null!; 

    }
}
