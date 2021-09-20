using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Teste.Models
{
    [Serializable()]
    [DataContract(Name = "TipoMensagem")]
    public enum TipoMensagem { Ok = 0, Erro = 1, Questao = 2, Atencao = 3, Confirmacao = 4 }
}
