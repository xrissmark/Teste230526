using System;
using System.Collections.Generic;
using System.Text;

namespace API.Domain.Messages
{
    public class MensagemErro
    {
        public static string CREDITO_VALOR_SUPERIOR_PERMITIDO = "Valor do crédito superior ao permitido. Valor máximo é 1.000.000";
        public static string CREDITO_NUMERO_PARCELAS = "Número de parcelas deve ser entre 5 e 72 vezes";
        public static string CREDITO_VALOR_INFERIOR_PJ = "Valor do crédito inferior ao permitido para o tipo de crédito selecionado. Valor mínimo é 15.000";
        public static string CREDITO_DATA_PRIMEIRO_VENCIMENTO = "Data de primeiro vencimento deve ser entre 15 e 40 dias da data vigente";
        public static string CREDITO_TIPO_INVALIDO = "Tipo de crédito inválido";
    }
}
