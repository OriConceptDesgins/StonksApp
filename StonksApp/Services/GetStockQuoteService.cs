using StonksApp.ServiceContracts;
using System.Text.Json;

namespace StonksApp.Services
{
    public class GetStockQuoteService : IGetStockQuoteService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public GetStockQuoteService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, object>> GetQuoteData(string stockSymbol)
        {
            HttpResponseMessage response;
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                Task<HttpResponseMessage> taskResponse = httpClient.GetAsync($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["Finnhubkey"]}");
                response = await taskResponse;
            }
            Stream stream = response.Content.ReadAsStream();

            StreamReader streamReader = new StreamReader(stream);

            string stringResponse = streamReader.ReadToEnd();

            Dictionary<string, object>? result = new Dictionary<string, object>();
            result = JsonSerializer.Deserialize<Dictionary<string, object>>(stringResponse);



            if (result != null)
            {
                if (result.ContainsKey("error"))
                {
                    throw new InvalidOperationException("Finnhub error.");
                }
                return result;
            }


            throw new InvalidOperationException("no response.");
        }
        }
}
