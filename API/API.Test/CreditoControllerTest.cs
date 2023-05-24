using API.Domain.Messages;
using API.Domain.Responses;
using API.Test.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Xunit;

namespace API.Test
{
    public class CreditoControllerTest 
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public CreditoControllerTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task LiberacaoCreditoOk()
        {
            var content = new CreditoMock();

            var stringContent = new StringContent(JsonConvert.SerializeObject(content));
            stringContent.Headers.ContentType.MediaType = "application/json";

            var mockedResponse = new LiberacaoCreditoResponseMock();

            var result = await _client.PostAsync("/Credito/LiberacaoCredito", stringContent);
            result.EnsureSuccessStatusCode();

            LiberacaoCreditoResponse response = JsonConvert.DeserializeObject<LiberacaoCreditoResponse>(await result.Content.ReadAsStringAsync());

            Assert.Equal(JsonConvert.SerializeObject(mockedResponse), JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async Task LiberacaoCreditoFail_Valor_Superior()
        {
            var content = new CreditoMock();
            content.Valor = 10000000.00M;

            var stringContent = new StringContent(JsonConvert.SerializeObject(content));
            stringContent.Headers.ContentType.MediaType = "application/json";

            var result = await _client.PostAsync("/Credito/LiberacaoCredito", stringContent);
            result.EnsureSuccessStatusCode();

            LiberacaoCreditoResponse response = JsonConvert.DeserializeObject<LiberacaoCreditoResponse>(await result.Content.ReadAsStringAsync());

            Assert.Equal(MensagemErro.CREDITO_VALOR_SUPERIOR_PERMITIDO, response.Mensagem);
        }
        [Fact]
        public async Task LiberacaoCreditoFail_QuantidadeParcelas_Inferior()
        {
            var content = new CreditoMock();
            content.QtdParcelas = 1;

            var stringContent = new StringContent(JsonConvert.SerializeObject(content));
            stringContent.Headers.ContentType.MediaType = "application/json";

            var result = await _client.PostAsync("/Credito/LiberacaoCredito", stringContent);
            result.EnsureSuccessStatusCode();

            LiberacaoCreditoResponse response = JsonConvert.DeserializeObject<LiberacaoCreditoResponse>(await result.Content.ReadAsStringAsync());

            Assert.Equal(MensagemErro.CREDITO_NUMERO_PARCELAS, response.Mensagem);
        }
        [Fact]
        public async Task LiberacaoCreditoFail_QuantidadeParcelas_Superior()
        {
            var content = new CreditoMock();
            content.QtdParcelas = 100;

            var stringContent = new StringContent(JsonConvert.SerializeObject(content));
            stringContent.Headers.ContentType.MediaType = "application/json";

            var result = await _client.PostAsync("/Credito/LiberacaoCredito", stringContent);
            result.EnsureSuccessStatusCode();

            LiberacaoCreditoResponse response = JsonConvert.DeserializeObject<LiberacaoCreditoResponse>(await result.Content.ReadAsStringAsync());

            Assert.Equal(MensagemErro.CREDITO_NUMERO_PARCELAS, response.Mensagem);
        }
        [Fact]
        public async Task LiberacaoCreditoFail_Valor_Inferior_PJ()
        {
            var content = new CreditoMock();
            content.Valor = 1000.00M;

            var stringContent = new StringContent(JsonConvert.SerializeObject(content));
            stringContent.Headers.ContentType.MediaType = "application/json";

            var result = await _client.PostAsync("/Credito/LiberacaoCredito", stringContent);
            result.EnsureSuccessStatusCode();

            LiberacaoCreditoResponse response = JsonConvert.DeserializeObject<LiberacaoCreditoResponse>(await result.Content.ReadAsStringAsync());

            Assert.Equal(MensagemErro.CREDITO_VALOR_INFERIOR_PJ, response.Mensagem);
        }
        [Fact]
        public async Task LiberacaoCreditoFail_DataPrimeiroVencimento_Inferior()
        {
            var content = new CreditoMock();
            content.DataPrimeiroVencimento = DateTime.Now;

            var stringContent = new StringContent(JsonConvert.SerializeObject(content));
            stringContent.Headers.ContentType.MediaType = "application/json";

            var result = await _client.PostAsync("/Credito/LiberacaoCredito", stringContent);
            result.EnsureSuccessStatusCode();

            LiberacaoCreditoResponse response = JsonConvert.DeserializeObject<LiberacaoCreditoResponse>(await result.Content.ReadAsStringAsync());

            Assert.Equal(MensagemErro.CREDITO_DATA_PRIMEIRO_VENCIMENTO, response.Mensagem);
        }
        [Fact]
        public async Task LiberacaoCreditoFail_DataPrimeiroVencimento_Superior()
        {
            var content = new CreditoMock();
            content.DataPrimeiroVencimento = DateTime.Now.AddDays(90);

            var stringContent = new StringContent(JsonConvert.SerializeObject(content));
            stringContent.Headers.ContentType.MediaType = "application/json";

            var result = await _client.PostAsync("/Credito/LiberacaoCredito", stringContent);
            result.EnsureSuccessStatusCode();

            LiberacaoCreditoResponse response = JsonConvert.DeserializeObject<LiberacaoCreditoResponse>(await result.Content.ReadAsStringAsync());

            Assert.Equal(MensagemErro.CREDITO_DATA_PRIMEIRO_VENCIMENTO, response.Mensagem);
        }
    }
}
