namespace RFMoneyMatters.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string? ClerckId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Streak { get; set; }
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public int Coins { get; set; }
        public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    }
}