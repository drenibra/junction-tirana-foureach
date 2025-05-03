namespace RFMoneyMatters.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsSet { get; set; }
        public double ProgressPercentage { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int RewardCoins { get; set; }
        public string PersonId { get; set; }
    }
}