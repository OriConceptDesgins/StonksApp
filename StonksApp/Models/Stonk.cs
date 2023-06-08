namespace StonksApp.Models
{
    public class Stonk
    {
        public double PrecentageChange { get; set; }
        public string Symbol { get; set; } = string.Empty; //technicall could give a default value here but the exercise is also about reading config.
        public double Value { get; set; }

        public string? Name { get; set; } = string.Empty;

        public string? Exchange{get; set; } = string.Empty;
        
    }
}
