using Amazon.Lambda.Model;
using Karapinha.DAL.Converters;
using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Karapinnha.DTO;
using Microsoft.AspNetCore.Http;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Services
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly IProfissionalRepository Repository;
        private readonly IProfissionalHorarioRepository HorarioRepository;

        public ProfissionalService(IProfissionalRepository repos, IProfissionalHorarioRepository horarioRepository)
        {
            Repository = repos;
            HorarioRepository = horarioRepository;
        }

        public async Task<ProfissionalDTO> CreateProfissional(ProfissionalDTO dto, IFormFile foto)
        {
            try
            {
                var profissionalAdded = ProfissionalConverter.ToProfissionalDTO(await Repository.CreateProfissional(ProfissionalConverter.ToProfissional(dto), foto));


                foreach (var horarioId in dto.Horarios)
                {
                    var horarioProfissional = new ProfissionalHorario
                    {
                        IdHorario = horarioId,
                        IdProfissional = profissionalAdded.IdProfissional,
                    };
                    await HorarioRepository.CreateProfissionalHorario(horarioProfissional);
                }

                return profissionalAdded;
            }
            catch (Exception ex) { 
                throw new ServiceException(ex.Message);
            }
            
        }

        public async Task<ProfissionalDTO> GetProfissionalById(int id)
        {
            try
            {
                return ProfissionalConverter.ToProfissionalDTO(await Repository.GetProfissionalById(id));
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task<IEnumerable<ProfissionalDTO>> GetAllProfissionals()
        {
            try
            {
                var profissionals = await Repository.GetAllProfissionals();
                return profissionals.Select(ProfissionalConverter.ToProfissionalDTO);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex);
            }
        }

        public IEnumerable<dynamic> GetAllProfissionaisByIdCategoria()
        {
            try
            {
                return Repository.GetAllProfissionaisByIdCategoria();
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex);
            }
        }

        public async Task<bool> DeleteProfissional(int id)
        {
            try
            {
                var prof = await GetProfissionalById(id);
                var profHorario = await HorarioRepository.GetProfissionalById(id);
                if (prof != null && profHorario!=null) {
                    await HorarioRepository.DeleteProfissionalHorario(id);
                    await Repository.DeleteProfissional(id);
                    return true;
                }
                return false;
            }
            catch (Exception ex) {
            
                throw new ServiceException(ex.Message);
            }
        }
    }
}
