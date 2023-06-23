namespace StonksApp.Models.Modelnterface
{
    public interface IStonkOrderModel
    {
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public double Price { get; set; }
    }
}
