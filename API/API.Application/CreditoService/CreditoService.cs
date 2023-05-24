using API.Domain.Entities;
using API.Domain.Enums;
using API.Domain.Messages;
using API.Domain.Responses;
using System;

namespace API.Application.CreditoService
{
    public class CreditoService : ICreditoService
    {
        public LiberacaoCreditoResponse LiberacaoCredito(Credito pedidoCredito)
        {
            var response = new LiberacaoCreditoResponse();

            if (pedidoCredito.Valor > 1000000)
            {
                response.Mensagem = MensagemErro.CREDITO_VALOR_SUPERIOR_PERMITIDO;
                return response;
            }                

            if (pedidoCredito.QtdParcelas < 5 || pedidoCredito.QtdParcelas > 72)
            {
                response.Mensagem = MensagemErro.CREDITO_NUMERO_PARCELAS;
                return response;
            }

            if (pedidoCredito.Tipo == TipoCreditoEnum.PessoaJuridica && pedidoCredito.Valor < 15000)
            {
                response.Mensagem = MensagemErro.CREDITO_VALOR_INFERIOR_PJ;
                return response;
            }

            if ((pedidoCredito.DataPrimeiroVencimento - DateTime.Now).TotalDays < 15 || 
                (pedidoCredito.DataPrimeiroVencimento - DateTime.Now).TotalDays > 40)
            {
                response.Mensagem = MensagemErro.CREDITO_DATA_PRIMEIRO_VENCIMENTO;
                return response;
            }

            CalcularJuros(pedidoCredito, ref response);            

            return response;
        }

        private void CalcularJuros (Credito pedidoCredito, ref LiberacaoCreditoResponse response)
        {
            decimal porcentagemJuros = 0;

            switch (pedidoCredito.Tipo)
            {
                case TipoCreditoEnum.Direto:
                    porcentagemJuros = 2;
                    break;
                case TipoCreditoEnum.Consignado:
                    porcentagemJuros = 1;
                    break;
                case TipoCreditoEnum.PessoaJuridica:
                    porcentagemJuros = 5;
                    break;
                case TipoCreditoEnum.PessoaFisica:
                    porcentagemJuros = 3;
                    break;
                case TipoCreditoEnum.Imobiliario:
                    porcentagemJuros = 9;
                    break;
                default:
                    response.Status = StatusCreditoEnum.Recusado;
                    response.Mensagem = MensagemErro.CREDITO_TIPO_INVALIDO;
                    return;
            }
            
            response.ValorJuros = decimal.Round((porcentagemJuros / 100) * pedidoCredito.Valor, 2, MidpointRounding.AwayFromZero); 
            response.ValorTotal = decimal.Round(pedidoCredito.Valor + response.ValorJuros, 2, MidpointRounding.AwayFromZero);
            response.Status = StatusCreditoEnum.Aprovado;
        }

    }
}
