namespace StonksApp.ServiceContracts
{
    public interface IGetStockQuoteService
    {
        Task<Dictionary<string, object>> GetQuoteData(string stockSymbol);
    }
}
