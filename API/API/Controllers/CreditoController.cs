using API.Application;
using API.Domain.Entities;
using API.Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditoController : ControllerBase
    {       
        private readonly ICreditoService _creditoService;

        public CreditoController(ICreditoService creditoService)
        {
            _creditoService = creditoService;
        }

        /// <summary>
        /// Faz um pedido de liberação de crédito.
        /// </summary>
        /// <param name="pedidoCredito"></param>
        /// <returns>O resultado do pedido</returns>
        /// <response code="200">Retorna se o pedido foi aceito ou rejeitado</response>
        [HttpPost("LiberacaoCredito")]
        public ActionResult<LiberacaoCreditoResponse> LiberacaoCredito(Credito pedidoCredito)
        {
            if (pedidoCredito == null)
                return BadRequest();

            var response = _creditoService.LiberacaoCredito(pedidoCredito);
            return Ok(response);
        }
    }
}
