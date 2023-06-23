using Microsoft.AspNetCore.Mvc;
using StonksApp.Services;
using StonksApp.Models;
using Microsoft.Extensions.Options;
using StonksApp.ServiceContracts;


namespace StonksApp.Controllers
{
    public class StonksController : Controller
    {
        private readonly IGetStockService _stockService;

        private readonly Stonk stonkModel;
        public StonksController(GetStockService stonksService,   IOptions<Stonk> stonkOptions)
        {
            _stockService = stonksService;
            stonkModel = stonkOptions.Value;
        }

        [Route("/")]
        public async Task<IActionResult> DisplayStonks()
        {

            Dictionary<string, object> quoteData = await _stockService.GetQuoteData(stonkModel.Symbol);
            Dictionary<string, object> descriptionData = await _stockService.GetDescriptionData(stonkModel.Symbol);
            
            stonkModel.Value = Convert.ToDouble(quoteData["c"].ToString());
            stonkModel.PrecentageChange = Convert.ToDouble(quoteData["dp"].ToString());

            stonkModel.Name = descriptionData["name"].ToString();
            stonkModel.Exchange = descriptionData["exchange"].ToString();

            return View("~/Views/Stonks/Index.cshtml", stonkModel);
        }
    }
}
