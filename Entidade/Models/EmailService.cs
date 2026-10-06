using Entidade.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidade.Models
{
    public class EmailService : IEmailService
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }

        public EmailService Enviar(string destinatario, string mensagem)
        {
            return new EmailService
            {
                Sucesso = true,
                Mensagem = $"E-mail enviado para {destinatario}"
            };
        }
    }
}
