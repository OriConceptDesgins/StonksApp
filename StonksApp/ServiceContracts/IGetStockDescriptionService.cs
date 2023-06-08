namespace StonksApp.ServiceContracts
{
    public interface IGetStockDescriptionService
    {
        Task<Dictionary<string, object>> GetDescriptionData(string symbol);
    }
}
