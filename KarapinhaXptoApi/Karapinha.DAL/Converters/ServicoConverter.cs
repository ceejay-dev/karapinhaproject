using Karapinha.Model;
using Karapinnha.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Converters
{
    public class ServicoConverter
    {
        public static Servico ToServico(ServicoDTO dto)
        {
            return new Servico
            {
                NomeServico = dto.NomeServico,
                Preco = dto.Preco,
                FkCategoria = dto.FkCategoria,
            };
        }

        public static ServicoDTO ToServicoDTO(Servico model)
        {
            return new ServicoDTO
            {
                NomeServico = model.NomeServico,
                Preco = model.Preco,
                FkCategoria = model.FkCategoria,
            };
        }

        public static Servico UpdateServico(ServicoUpdateDTO update, Servico servico)
        {
            servico.NomeServico = update.NomeServico;
            servico.Preco = update.Preco;
            servico.FkCategoria = update.FkCategoria;

            return servico;
        }
    }
}
