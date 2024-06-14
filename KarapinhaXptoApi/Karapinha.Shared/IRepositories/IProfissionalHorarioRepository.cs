using Karapinha.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IRepositories
{
    public interface IProfissionalHorarioRepository
    {
        Task<ProfissionalHorario> CreateProfissionalHorario(ProfissionalHorario profissionalHorario);
        Task<ProfissionalHorario> GetProfissionalById(int id);
        Task<bool> DeleteProfissionalHorario(int id);
    }
}
