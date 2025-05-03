namespace RFMoneyMatters.DTOs
{
    public class ExpenseSummaryDto
    {
        public decimal Total { get; set; }
        public decimal Needs { get; set; }
        public decimal Wants { get; set; }
        public double NeedsPercentage { get; set; }
        public double WantsPercentage { get; set; }
    }

}
