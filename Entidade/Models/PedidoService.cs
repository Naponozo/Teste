using Entidade.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidade.Models
{
    // 3. Classe que depende do IEmailService
    public class PedidoService
    {
        private readonly IEmailService _emailService;

        public PedidoService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public EmailService CriarPedido()
        {
            // Lógica do pedido...

            return _emailService.Enviar(
                "cliente@email.com",
                "Seu pedido foi criado!"
            );
            
        }
    }
}
