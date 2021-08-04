using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Waffle.Inc.Models
{
    public class LoginResponseModel
    {
        public bool Autenticado { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataExpiracao { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
        public string Role { get; set; }

        public LoginResponseModel(bool autenticado, int minutosDuracao)
        {
            Autenticado = autenticado;
            if (Autenticado)
            {
                DataCriacao = DateTime.Now;
                DataExpiracao = DataCriacao +
                    TimeSpan.FromMinutes(minutosDuracao);
                Message = "Ok";
            }
            else
            {
                Message = "Senha inválida";
            }
        }

        public void SetToken(string accessToken)
        {
            AccessToken = accessToken;
        }

        public void SetRole(string role)
        {
            Role = role;
        }
    }
}
