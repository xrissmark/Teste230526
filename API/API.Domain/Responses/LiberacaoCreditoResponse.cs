using API.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Domain.Responses
{
    public class LiberacaoCreditoResponse
    {
        public StatusCreditoEnum Status { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorJuros { get; set; }
        public string Mensagem { get; set; }
    }
}
