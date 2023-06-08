namespace StonksApp.ServiceContracts
{
    public interface IGetStockService
    {
        Task<Dictionary<string, object>> GetQuoteData(string stockSymbol);
        Task<Dictionary<string, object>> GetDescriptionData(string symbol);
    }
}
