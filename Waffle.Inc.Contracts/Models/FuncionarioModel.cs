using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Waffle.Inc.Contracts.Models
{
    public class FuncionarioModel
    {
        public int? Id { get; set; }

        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sobrenome { get; set; }
        [Required]
        [RegularExpression(@".+?waffle-inc\.com$",
            ErrorMessage = "E-mail inválido, o email precisa ser corporativo '@waffle-inc.com'.")]
        public string Email { get; set; }
        [Required]
        public string NumeroChapa { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string Role { get; set; }

        public IEnumerable<TelefoneModel> Telefones { get; set; }

        public int? LiderId { get; set; }
        public string LiderNome { get; set; }
    }
}
