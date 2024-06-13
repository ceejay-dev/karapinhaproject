using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinnha.DTO
{
    public class ServicoUpdateDTO
    {
        public int IdServico { get; set; }
        public string? NomeServico { get; set; }
        public double Preco { get; set; }
        public int FkCategoria { get; set; }
    }
}
