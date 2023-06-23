using StonksApp.Models.DTO;
using StonksApp.Models.Modelnterface;
using StonksApp.ServiceContracts;
using StonksApp.Services;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Xunit.Abstractions;

namespace TestProject
{
    public class StockTradeServiceTest
    {
        private readonly IStockTradeService _stonkService;
        private readonly ITestOutputHelper _testOutputHelper;

        private readonly BuyOrderRequest _buyOrderRequest;
        private readonly SellOrderRequest _sellOrderRequest;
        private readonly BuyOrderRequest _buyOrderRequestBad;
        private readonly SellOrderRequest _sellOrderRequestBad;

        public StockTradeServiceTest(ITestOutputHelper testOutputHelper)
        {
            _stonkService = new StockTradeService();
            _testOutputHelper = testOutputHelper;

            _buyOrderRequestBad = new BuyOrderRequest()
            {
                StockSymbol = string.Empty,
                StockName = string.Empty,
                DateAndTimeOfOrder = new DateTime(1992, 10, 31),
                Quantity = 0,
                Price = 0,
            };
            _sellOrderRequestBad = new SellOrderRequest()
            {
                StockSymbol = string.Empty,
                StockName = string.Empty,
                DateAndTimeOfOrder = new DateTime(1992, 10, 31),
                Quantity = 0,
                Price = 0,
            };
            _sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "APPL",
                StockName = "Apple Inc",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 10,
                Price = 1,
            };
            _buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft Corp",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity= 10,
                Price = 333.60,
            };
        }
            
        

        #region CreateBuyOrder
        [Fact]
        public async void CreateBuyOrder_NullRequestTest( ) 
        {
            BuyOrderRequest? request = null;
    
            await Assert.ThrowsAsync<ArgumentNullException>( async () => 
            {
                await _stonkService.CreateBuyOrder(request);
            });
        }

        /// <summary>
        /// The goal for this test is to provide faulty data and see that we get error in results.
        /// </summary>
        [Fact]
        public async void CreateBuyOrder_DataValidationTest() 
        {

            Task<BuyOrderResponse>? response = _stonkService.CreateBuyOrder(_buyOrderRequestBad);
            BuyOrderResponse? result;

            result = (response == null) ? null : await response;
             
            if (result != null) 
            {
                _testOutputHelper.WriteLine($"{GetValidationResults(_buyOrderRequestBad).Count}");
                _testOutputHelper.WriteLine($"{GetValidationResults(_buyOrderRequestBad)[2]}");
                Assert.True((GetValidationResults(_buyOrderRequestBad).Count > 0) &&
                            (GetValidationResults(result).Count > 0));
            }
                        
            // but I need another assertion here for after the create buy order.

        }

        [Fact]
        public async void CreateBuyOrder_ProperInputTest() 
        {    

            Task<BuyOrderResponse>? response = _stonkService.CreateBuyOrder(_buyOrderRequest);
            BuyOrderResponse? result;
            result = (response == null) ? null : await response;

            Assert.True(GetValidationResults(result).Count == 0);
        }
        #endregion

        #region CreateSellOrder
        [Fact]
        public async void CreateSellOrder_NullRequestTest() 
        {
            SellOrderRequest? request = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _stonkService.CreateSellOrder(request);
            });
        }

        [Fact]
        public async void CreateSellOrder_DataValidationTest() 
        {
            Task<SellOrderResponse>? response = _stonkService.CreateSellOrder(_sellOrderRequestBad);
            SellOrderResponse? result;

            result = (response == null) ? null : await response;

            if (result != null)
            {
                _testOutputHelper.WriteLine($"{GetValidationResults(_sellOrderRequestBad).Count}");
                Assert.True((GetValidationResults(_sellOrderRequestBad).Count > 0) &&
                            (GetValidationResults(result).Count > 0));
            }
        }

        [Fact]
        public async void CreateSellOrder_ProperInputTest() 
        {

            Task<SellOrderResponse>? response = _stonkService.CreateSellOrder(_sellOrderRequest);
            SellOrderResponse? result;

            result = (response == null) ? null : await response;

            if (result != null)
            {
                _testOutputHelper.WriteLine($"{GetValidationResults(_sellOrderRequest).Count} Date: {DateTime.Now}");
                Assert.True((GetValidationResults(_sellOrderRequest).Count == 0) &&
                            (GetValidationResults(result).Count == 0));
            }
        }

        // All Validation tests should be done via attributes on the model (Range , Required etc)
        #endregion

        #region GetBuyOrders
        [Fact]
        public async void GetBuyOrders_CheckIfCreatedEmpty()
        {
            StockTradeService stonkServiceTest = new();
            List<BuyOrderResponse> results = await stonkServiceTest.GetBuyOrders();
            Assert.Empty(results);
        }

        [Fact]
        public async void GetBuyOrders_ReturnCorrectListTest()
        {
            StockTradeService stonkServiceTest = new();

            await stonkServiceTest.CreateBuyOrder(_buyOrderRequest);
            List<BuyOrderResponse> results = await stonkServiceTest.GetBuyOrders();
            Assert.NotEmpty(results);
        }
        #endregion

        #region GetSellOrders
        [Fact]
        public async void GetSellOrders_CheckIfCreatedEmpty() 
        {
            StockTradeService stonkServiceTest = new();
            List<SellOrderResponse> results = await stonkServiceTest.GetSellOrders();
            Assert.Empty(results);
        }

        [Fact]
        public async void GetSellOrders_ReturnCorrectListTest()
        {
            StockTradeService stonkServiceTest = new();

            await stonkServiceTest.CreateSellOrder(_sellOrderRequest); 
            
            List<SellOrderResponse> results = await stonkServiceTest.GetSellOrders();
            Assert.NotEmpty(results);
        }
        #endregion


        #region utilMethods

        /// <summary>
        /// Utility method to validate an instance of a DTO with validations.
        /// Uses DataAnnotations Validator.TryValidateObject()
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private List<ValidationResult> GetValidationResults(IStonkOrderModel? request)
        {
            if (request == null) 
            {
                return new List<ValidationResult>();
            }
            ValidationContext validationContext = new ValidationContext(request);
            List<ValidationResult> results = new();
            Validator.TryValidateObject(request, validationContext, results, true);

            return results;
        }
        #endregion
    }
}