using Microsoft.AspNetCore.Mvc;
using StonksApp.Services;
using StonksApp.Models;
using Microsoft.Extensions.Options;

namespace StonksApp.Controllers
{
    public class StonksController : Controller
    {
        private readonly GetStockQuoteService _stockQuoteService;
        private readonly GetStockDescriptionService _stockDescriptionService;
        private readonly Stonk stonkModel;
        public StonksController(GetStockQuoteService stonksQuote, GetStockDescriptionService stonksDescription,  IOptions<Stonk> stonkOptions)
        {
            _stockQuoteService = stonksQuote;
            _stockDescriptionService = stonksDescription;
            stonkModel = stonkOptions.Value;
        }

        [Route("/")]
        public async Task<IActionResult> DisplayStonks()
        {

            Dictionary<string, object> quoteData = await _stockQuoteService.GetQuoteData(stonkModel.Symbol);
            Dictionary<string, object> descriptionData = await _stockDescriptionService.GetDescriptionData(stonkModel.Symbol);
            
            stonkModel.Value = Convert.ToDouble(quoteData["c"].ToString());
            stonkModel.PrecentageChange = Convert.ToDouble(quoteData["dp"].ToString());

            stonkModel.Name = descriptionData["name"].ToString();
            stonkModel.Exchange = descriptionData["exchange"].ToString();

            return View("~/Views/Stonks/Index.cshtml", stonkModel);
        }
    }
}
