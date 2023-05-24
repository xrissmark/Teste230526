using API.Domain.Entities;
using API.Domain.Responses;

namespace API.Application
{
    public interface ICreditoService
    {
        LiberacaoCreditoResponse LiberacaoCredito(Credito credito);
    }
}
