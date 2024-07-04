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
                var profissional = ProfissionalConverter.ToProfissional(dto);
                var profissionalAdded = await Repository.CreateProfissional(profissional, foto);

                foreach (var horarioId in dto.Horarios)
                {
                    var horarioProfissional = new ProfissionalHorario
                    {
                        IdProfissional = profissionalAdded.IdProfissional,
                        IdHorario = horarioId
                    };
                    await HorarioRepository.CreateProfissionalHorario(horarioProfissional);
                }

                return ProfissionalConverter.ToProfissionalDTO(profissionalAdded);
            }
            catch (Exception ex)
            {
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

        public async Task<ProfissionalDTO> GetProfissionalByIdCategoria(int id)
        {
            try
            {
                return ProfissionalConverter.ToProfissionalDTO(await Repository.GetProfissionalByIdCategoria(id));
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

        public Task<IEnumerable<dynamic>> GetAllProfissionaisByIdCategoria(int idCategoria)
        {
            try
            {
                return Repository.GetAllProfissionaisByIdCategoria(idCategoria);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex);
            }
        }

        public IEnumerable<dynamic> GetAllProfissionaisWithCategoria()
        {
            try
            {
                return Repository.GetAllProfissionaisWithCategoria();
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
                if (prof != null && profHorario != null)
                {
                    await HorarioRepository.DeleteProfissionalHorario(id);
                    await Repository.DeleteProfissional(id);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new ServiceException(ex.Message);
            }
        }
    }
}
