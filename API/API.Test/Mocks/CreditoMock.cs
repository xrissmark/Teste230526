using API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Test.Mocks
{
    public class CreditoMock: Credito
    {
        public CreditoMock()
        {
            DataPrimeiroVencimento = DateTime.Now.AddDays(20);
            QtdParcelas = 12;
            Tipo = Domain.Enums.TipoCreditoEnum.PessoaJuridica;
            Valor = 50000;
        }
    }
}
