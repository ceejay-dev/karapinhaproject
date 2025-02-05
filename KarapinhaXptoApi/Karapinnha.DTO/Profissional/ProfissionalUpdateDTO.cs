﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinnha.DTO.Profissional
{
    public class ProfissionalUpdateDTO
    {
        public int IdProfissional { get; set; }
        public string? NomeProfissional { get; set; }
        public int FkCategoria { get; set; }
        public string? EmailProfissional { get; set; }
        public string? FotoProfissional { get; set; }
        public string? BilheteProfissional { get; set; }
        public string? TelemovelProfissional { get; set; }
        public List<int> Horarios { get; set; } = new List<int>();
    }
}
