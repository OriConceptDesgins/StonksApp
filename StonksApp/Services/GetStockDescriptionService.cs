using StonksApp.ServiceContracts;
using System.Text.Json;

namespace StonksApp.Services
{
    public class GetStockDescriptionService : IGetStockDescriptionService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public GetStockDescriptionService(IHttpClientFactory httpClientFactory , IConfiguration configuration) 
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory; 
        }

        public async Task<Dictionary<string, object>> GetDescriptionData(string symbol)
        {
            HttpResponseMessage httpResponse;
            using (HttpClient client = _httpClientFactory.CreateClient()) 
            {
                Task<HttpResponseMessage> asyncResponse = client.GetAsync($"https://finnhub.io/api/v1/stock/profile2?symbol={symbol}&token={_configuration["FinnhubKey"]}");
                httpResponse = await asyncResponse;
            }
            
            Stream stream = httpResponse.Content.ReadAsStream();
            StreamReader streamReader = new StreamReader(stream);
            string responseData =  streamReader.ReadToEnd();
            Dictionary<string, object>? result = new(); 
            result = JsonSerializer.Deserialize<Dictionary<string, object>>(responseData);
            
            if (result != null )
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
