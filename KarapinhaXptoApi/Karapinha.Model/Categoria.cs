using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Model
{
    [Table("categorias")]
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string? NomeCategoria { get; set; }
    }
}
