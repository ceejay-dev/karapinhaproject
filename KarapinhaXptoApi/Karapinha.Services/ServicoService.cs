using Amazon.Lambda.Model;
using Karapinha.DAL.Converters;
using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Karapinnha.DTO.Servico;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Services
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository Repository;

        public ServicoService(IServicoRepository repository)
        {
            Repository = repository;
        }
        public async Task<ServicoDTO> CreateTreatment(ServicoDTO servico)
        {
            try
            {
                return ServicoConverter.ToServicoDTO(await Repository.CreateTreatment(ServicoConverter.ToServico(servico)));
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.ToString());
            }
        }

        public async Task<ServicoDTO> GetTreatementById(int id)
        {
            try
            {
                var servico = await Repository.GetTreatmentById(id);
                return ServicoConverter.ToServicoDTO(servico);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task<IEnumerable<ServicoDTO>> GetAllTreatments()
        {
            try
            {
                var servico = await Repository.GetAllTreatments();
                return servico.Select(ServicoConverter.ToServicoDTO);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task<bool> DeleteTreatment(int id)
        {
            try
            {
                return await Repository.DeleteTreatment(id);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task UpdateTreatment(ServicoUpdateDTO update)
        {
            try
            {
                var servico = await Repository.GetTreatmentById(update.IdServico);
                if (servico == null) throw new NotFoundException();

                await Repository.UpdateTreatment(ServicoConverter.UpdateServico(update, servico));
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.ToString());
            }
        }

        public IEnumerable<dynamic> GetAllServicosByIdCategoria()
        {
            try
            {
                return Repository.GetAllServicosByIdCategoria();
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }

        }

        public async Task<IEnumerable<ServicoDTO>> GetMostRequestedTreatments()
        {
            try
            {
                var result = await Repository.GetMostRequestedTreatments();
                return result.Select(ServicoConverter.ToServicoDTO);
            }
            catch (Exception ex) { 
                throw new ServiceException(ex.Message);
            }
        }
    }
}
