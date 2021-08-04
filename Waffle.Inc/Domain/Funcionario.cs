using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Waffle.Inc.Domain
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string NumeroChapa { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public ICollection<Telefone> Telefones { get; set; }

        [ForeignKey("Lider")]
        public int? LiderId { get; set; }
        public string LiderNome { get; set; }
    }
}
