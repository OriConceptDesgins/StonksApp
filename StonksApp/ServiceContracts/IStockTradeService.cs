using StonksApp.Models.DTO;

namespace StonksApp.ServiceContracts
{
    public interface IStockTradeService
    {
        Task<BuyOrderResponse>? CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

        Task<SellOrderResponse>? CreateSellOrder(SellOrderRequest? sellOrderRequest);

        Task<List<BuyOrderResponse>> GetBuyOrders();

        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
