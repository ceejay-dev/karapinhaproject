using Amazon.Lambda.Model;
using Karapinha.DAL.Converters;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Karapinnha.DTO.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<CategoriaDTO> CreateCategory(CategoriaDTO dto)
        {
            try
            {
                var category = dto;
                category.Estado = true;
                category = CategoriaConverter.ToCategoriaDTO(await _categoriaRepository.CreateCategory(CategoriaConverter.ToCategoria(dto)));
                return category;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task<CategoriaDTO> GetCategoryById(int id)
        {
            try
            {
                return CategoriaConverter.ToCategoriaDTO(await _categoriaRepository.GetCategoryById(id));
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task<IEnumerable<CategoriaDTO>> GetAllCategories()
        {
            try
            {
                var categories = await _categoriaRepository.GetAllCategories();
                return categories.Select(CategoriaConverter.ToCategoriaDTO);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                await _categoriaRepository.DeleteCategory(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task UpdateCategory(CategoriaUpdateDTO dto)
        {
            try
            {
                var category = await _categoriaRepository.GetCategoryById(dto.IdCategoria);
                if (category == null) return;

                await _categoriaRepository.UpdateCategory(CategoriaConverter.UpdateCategoria(dto, category));
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        }
    }
