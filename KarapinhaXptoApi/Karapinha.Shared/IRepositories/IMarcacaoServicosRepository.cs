using Karapinha.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IRepositories
{
    public interface IMarcacaoServicosRepository
    {
        Task<MarcacaoServico> CreateProfissionalHorario(MarcacaoServico marcacaoServico);
    }
}
