using RFMoneyMatters.Models;

namespace RFMoneyMatters.DTOs
{
    public class PersonDto
    {
        public string? ClerckId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Streak { get; set; }
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public int Coins { get; set; }
        public Goal Goals { get; set; }
    }
}