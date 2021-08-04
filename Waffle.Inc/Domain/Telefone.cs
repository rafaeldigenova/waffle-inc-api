﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waffle.Inc.Domain
{
    public class Telefone
    {
        public int Id { get; set; }
        public string CodigoDeArea { get; set; }
        public string Numero { get; set; }

        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }
}
