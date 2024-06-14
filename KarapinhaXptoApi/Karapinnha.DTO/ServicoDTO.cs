using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinnha.DTO
{
    public class ServicoDTO
    {
        public int IdServico { get; set; }
        public string? NomeServico { get; set; }
        public double Preco { get; set; }
        public int FkCategoria { get; set; }
    }
}
