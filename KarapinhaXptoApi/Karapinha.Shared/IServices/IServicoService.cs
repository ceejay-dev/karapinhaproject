using Karapinha.Model;
using Karapinnha.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IServices
{
    public interface IServicoService
    {
        Task<ServicoDTO> CreateTreatment(ServicoDTO servico);
        Task<ServicoDTO> GetTreatementById(int id);
        Task<IEnumerable<ServicoDTO>> GetAllTreatments();
        Task<bool> DeleteTreatment(int id);
        Task UpdateTreatment(ServicoUpdateDTO servico);
        IEnumerable<dynamic> GetAllServicosByIdCategoria();
    }
}
