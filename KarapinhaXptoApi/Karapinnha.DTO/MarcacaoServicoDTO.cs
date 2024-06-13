using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinnha.DTO
{
    public class MarcacaoServicoDTO
    {
        public int Id { get; set; }
        public int FkMarcacao { get; set; }
        public int FkCategoria { get; set; }
        public int FkServico { get; set; }
        public int FkProfissional { get; set; }
    }
}
