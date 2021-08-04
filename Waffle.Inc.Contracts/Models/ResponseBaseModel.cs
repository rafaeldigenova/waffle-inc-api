using System;
using System.Collections.Generic;
using System.Text;

namespace Waffle.Inc.Contracts.Models
{
    public class ResponseBaseModel<T>
    {
        public bool ExecutadoComSucesso { get; set; }

        public string Mensagem { get; set; }

        public T CorpoDaResposta { get; set; }

        public ResponseBaseModel(bool executadoComSucesso, string mensagem, T corpoDaResposta)
        {
            ExecutadoComSucesso = executadoComSucesso;
            Mensagem = mensagem;
            CorpoDaResposta = corpoDaResposta;
        }
    }
}
