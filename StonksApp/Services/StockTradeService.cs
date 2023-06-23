using StonksApp.Models.DTO;
using StonksApp.ServiceContracts;
using Entities;
using StonksApp.Models.Modelnterface;

namespace StonksApp.Services
{
    public class StockTradeService : IStockTradeService
    {
        private List<BuyOrder> _buyOrdersList;
        private List<SellOrder> _sellOrdersList;

        public StockTradeService()
        {
            _buyOrdersList = new();
            _sellOrdersList = new();
        }
        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null)
            {
                throw new ArgumentNullException($" {nameof(buyOrderRequest)} is null, bad ");
            }
            else
            {

                BuyOrderResponse response = new BuyOrderResponse()
                {
                    BuyOrderID = Guid.NewGuid(),
                    StockName = buyOrderRequest.StockName,
                    StockSymbol = buyOrderRequest.StockSymbol,
                    DateAndTimeOfOrder = buyOrderRequest.DateAndTimeOfOrder,
                    Quantity = buyOrderRequest.Quantity,
                    Price = buyOrderRequest.Price,
                };

                AddToBuyOrdersList(response);

                return Task.FromResult(response);
            } 
        }

        public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException($" {nameof(sellOrderRequest)} is null, bad ");
            }

            SellOrderResponse response = new SellOrderResponse()
            { 
                SellOrderID = Guid.NewGuid(),
                StockName = sellOrderRequest.StockName,
                StockSymbol = sellOrderRequest.StockSymbol,
                DateAndTimeOfOrder = sellOrderRequest.DateAndTimeOfOrder,
                Quantity = sellOrderRequest.Quantity,
                Price = sellOrderRequest.Price,
            };

            AddToSellOrdersList(response);

            return Task.FromResult(response);
        }

        public Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrderResponse> response = new List<BuyOrderResponse>();
            foreach (BuyOrder order  in _buyOrdersList)
            {
                response.Add(ToBuyOrderResponse(order));
            }

            return Task.FromResult(response);
        }

        public Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrderResponse> response = new List<SellOrderResponse>();
            foreach (SellOrder order in _sellOrdersList)
            {
                response.Add(ToSellOrderResponse(order));
            }

            return Task.FromResult(response);
        }

        private void AddToBuyOrdersList(BuyOrderResponse order) 
        {
            _buyOrdersList.Add
                ( 
                    new BuyOrder() 
                    {
                        BuyOrderID = order.BuyOrderID,
                        StockName = order.StockName,
                        StockSymbol = order.StockSymbol,
                        DateAndTimeOfOrder= order.DateAndTimeOfOrder,
                        Quantity = order.Quantity,
                        Price = order.Price
                    }
                );
        }

        private void AddToSellOrdersList(SellOrderResponse order)
        {
            _sellOrdersList.Add
                (
                    new SellOrder()
                    {
                        SellOrderID = order.SellOrderID,
                        StockName = order.StockName,
                        StockSymbol = order.StockSymbol,
                        DateAndTimeOfOrder = order.DateAndTimeOfOrder,
                        Quantity = order.Quantity,
                        Price = order.Price
                    }
                );
        }

        private BuyOrderResponse ToBuyOrderResponse(BuyOrder order)
        {
            return new BuyOrderResponse()
            {
                BuyOrderID = order.BuyOrderID,
                StockName = order.StockName,
                StockSymbol = order.StockSymbol,
                DateAndTimeOfOrder = order.DateAndTimeOfOrder,
                Quantity = order.Quantity,
                Price = order.Price
            };
        }

        private SellOrderResponse ToSellOrderResponse (SellOrder order)
        {
            return new SellOrderResponse()
            {
                SellOrderID = order.SellOrderID,
                StockName = order.StockName,
                StockSymbol = order.StockSymbol,
                DateAndTimeOfOrder = order.DateAndTimeOfOrder,
                Quantity = order.Quantity,
                Price = order.Price
            };
        }
    }
}
