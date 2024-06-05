﻿using Karapinha.Model;
using Karapinnha.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Converters
{
    public class CategoriaConverter
    {
        public static Categoria ToCategoria(CategoriaDTO category)
        {
            return new Categoria
            {
                NomeCategoria = category.NomeCategoria
            };
        }

        public static CategoriaDTO ToCategoriaDTO(Categoria category)
        {
            return new CategoriaDTO
            {
                NomeCategoria = category.NomeCategoria
            };
        }

        public static Categoria UpdateCategoria(CategoriaUpdateDTO update, Categoria categoria)
        {
            categoria.NomeCategoria = update.NomeCategoria;
            return categoria;
        }
    }
}