using API.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Test.Mocks
{
    public class LiberacaoCreditoResponseMock : LiberacaoCreditoResponse
    {
        public LiberacaoCreditoResponseMock()
        {
            Status = Domain.Enums.StatusCreditoEnum.Aprovado;
            ValorJuros = 2500.00M;
            ValorTotal = 52500.00M;
            Mensagem = null;
        }
    }
}
