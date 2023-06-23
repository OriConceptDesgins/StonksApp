using StonksApp.Models.Modelnterface;
using System.ComponentModel.DataAnnotations;

namespace StonksApp.Models.DTO
{
    public class SellOrderResponse : IStonkOrderModel
    {
        public Guid SellOrderID { get; set; }

        [Required]
        public string? StockSymbol { get; set; }
        [Required]
        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1, 100000)]
        public uint Quantity { get; set; }
        [Range(1, 10000)]
        public double Price { get; set; }

        public double TradeAmount { get; set; }
    }
}
