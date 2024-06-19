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
                IdServico = dto.IdServico,
                NomeServico = dto.NomeServico,
                Preco = dto.Preco,
                FkCategoria = dto.FkCategoria,
            };
        }

        public static ServicoDTO ToServicoDTO(Servico model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "O modelo de serviço é nulo.");
            }

            return new ServicoDTO
            {
                IdServico = model.IdServico,
                NomeServico = model.NomeServico,
                Preco = model.Preco,
                FkCategoria = model.FkCategoria,
            };
        }

        public static Servico UpdateServico(ServicoUpdateDTO update, Servico servico)
        {
            servico.IdServico = update.IdServico;
            servico.NomeServico = update.NomeServico;
            servico.Preco = update.Preco;

            return servico;
        }
    }
}
