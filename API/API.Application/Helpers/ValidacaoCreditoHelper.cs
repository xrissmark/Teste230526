using System;
using System.Collections.Generic;
using System.Text;

namespace API.Application.Helpers
{
    public class ValidacaoCreditoHelper
    {
        public static decimal CREDITO_MAXVALUE = 1000000.00M;
        public static decimal CREDITO_PJ_MINVALUE = 15000.00M;

        public static int CREDITO_QTD_PARCELAS_MIN = 5;
        public static int CREDITO_QTD_PARCELAS_MAX = 72;

        public static int CREDITO_DT_VENCIMENTO_MIN = 15;
        public static int CREDITO_DT_VENCIMENTO_MAX = 40;
    }
}
