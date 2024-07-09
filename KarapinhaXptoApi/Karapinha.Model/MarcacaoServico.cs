using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Model
{
    [Table ("marcacoesServicos")]
    public class MarcacaoServico
    {
        [Key] 
        public int Id { get; set; }
        [ForeignKey(nameof(FkMarcacao))]
        public int FkMarcacao { get; set; }
        public Marcacao ? Booking { get; set; }
        [ForeignKey(nameof(FkServico))]
        public int FkServico { get; set; }
        public Servico ? Service { get; set; }
        [ForeignKey(nameof(FkProfissional))]
        public int FkProfissional { get; set; }
        public Profissional ? Profissional { get; set; }
        [ForeignKey(nameof(HoraMarcacao))]
        public int HoraMarcacao { get; set; }
        public Horario ? Horario { get; set; }
    }
}
