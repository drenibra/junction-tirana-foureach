using RFMoneyMatters.Models;

namespace RFMoneyMatters.DTOs
{
    public class GoalDto
    {
        public string Description { get; set; }
        public bool IsSet { get; set; }
        public double ProgressPercentage { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int RewardCoins { get; set; }
        public int PersonId { get; set; }
    }

    public class CreateGoalDto
    {
        public string Description { get; set; }
        public int RewardCoins { get; set; }
        public int PersonId { get; set; }
    }

    public class UpdateGoalDto
    {
        public string? Description { get; set; }
        public bool? IsSet { get; set; }
        public double? ProgressPercentage { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int? RewardCoins { get; set; }
    }
}