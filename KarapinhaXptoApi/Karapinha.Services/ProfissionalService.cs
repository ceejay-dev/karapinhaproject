using Amazon.Lambda.Model;
using Karapinha.DAL.Converters;
using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Karapinnha.DTO.Profissional;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public ProfissionalService(IProfissionalRepository repos)
        {
            Repository = repos;
        }

        public async Task<ProfissionalDTO> CreateProfissional(ProfissionalDTO dto, IFormFile foto)
        {
            try
            {
                //Armazenamento da fotografia do profissional
                string photoPath = null;
                if (foto != null)
                {
                    var uploadsFolder = Path.Combine("wwwroot", "storage");
                    // Verifica se o diretório existe e cria se necessário
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Console.WriteLine("");
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Gera o caminho completo para o arquivo
                    photoPath = Path.Combine(uploadsFolder, Guid.NewGuid() + Path.GetExtension(foto.FileName));
                    // Salva o arquivo no caminho especificado
                    using (var fileStream = new FileStream(photoPath, FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                    }
                    // Ajusta o caminho para ser usado em URLs
                    photoPath = "/" + photoPath.Replace("wwwroot\\", string.Empty).Replace("\\", "/");
                }
                var profissionalAdded = dto;
                //Adição da foto submetida ao profissional
                profissionalAdded.FotoProfissional = photoPath;
                profissionalAdded = ProfissionalConverter.ToProfissionalDTO(await Repository.CreateProfissional(ProfissionalConverter.ToProfissional(profissionalAdded)));
                return profissionalAdded;
            }
            catch (ServiceException ex)
            {
                Console.WriteLine(ex.ToString());
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
                var profissional = await Repository.GetProfissionalById(id);

                if (profissional != null)
                {
                    // Define o estado do profissional como falso
                    profissional.Estado = false;

                    // Salva as alterações no banco de dados
                    await Repository.UpdateProfissional(profissional);
                    Console.WriteLine("Profissional removido com sucesso.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Profissional não encontrado.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task<IEnumerable<ProfissionalDTO>> GetMostRequestedProfessionals()
        {
            try
            {
                var result = await Repository.GetMostRequestedProfessionals();
                return result.Select(ProfissionalConverter.ToProfissionalDTO);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
    }
}
