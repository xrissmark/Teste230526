using System;
using API.Domain.Enums;

namespace API.Domain.Entities
{
    public class Credito
    {
        public decimal Valor { get; set; }
        public TipoCreditoEnum Tipo { get; set; }
        public int QtdParcelas { get; set; }
        public DateTime DataPrimeiroVencimento { get; set; }
    }
}
