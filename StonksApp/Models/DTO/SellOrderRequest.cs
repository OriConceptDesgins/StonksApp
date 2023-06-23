using StonksApp.Models.Modelnterface;
using System.ComponentModel.DataAnnotations;
using ValidationUtils.DataAnnotations;

namespace StonksApp.Models.DTO
{
    public class SellOrderRequest : IStonkOrderModel
    {
        [Required]
        public string? StockSymbol { get; set; }
        [Required]
        public string? StockName { get; set; }

        [DateTimeRange("2000/01/01", "max")]
        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1, 100000)]
        public uint Quantity { get; set; }
        [Range(1, 10000)]
        public double Price { get; set; }


    }
}
