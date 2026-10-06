using Entidade.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidade.Interface
{
    public interface IEmailService
    {
        EmailService Enviar(string destinatario, string mensagem);
    }
}
