using Microsoft.AspNetCore.Mvc;
using StonksApp.ServiceContracts;
using StonksApp.Services;
using StonksApp.Models;

namespace StonksApp.Controllers
{
    public class TradeController : Controller
    {
        IStockTradeService _stockTradeService;

        public TradeController(StockTradeService stockTradeService) 
        {
            _stockTradeService = stockTradeService;
        }

        [Route("/trade")]
        public IActionResult StonkTrade()
        {
            StonkTrade stonkTrade = new StonkTrade();
            return View("~/Views/Trade/StockTrade.cshtml", stonkTrade);
        }
    }
}
