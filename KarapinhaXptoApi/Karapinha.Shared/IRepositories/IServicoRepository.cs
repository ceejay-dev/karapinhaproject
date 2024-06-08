using Karapinha.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IRepositories
{
    public interface IServicoRepository
    {
        Task <Servico> CreateTreatment (Servico servico);
        Task <Servico> GetTreatmentById (int id);
        Task<List<Servico>> GetAllTreatments();
        Task<bool> DeleteTreatment(int id);
        Task UpdateTreatment(Servico servico);
    }
}
