using RFMoneyMatters.Models;

namespace RFMoneyMatters.DTOs
{
    public class PersonDto
    {
        public string? ClerckId { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }

        public int Streak { get; set; }
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public DateTime? LastActiveDate { get; set; }

        public int Coins { get; set; }
        public ICollection<Goal> Goals { get; set; } = new List<Goal>();
        public ICollection<ExpenseDto> Expenses { get; set; } = new List<ExpenseDto>();

    }

    public class CreatePersonDto
    {
        public string? ClerckId { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
    }

    public class UpdatePersonDto
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public int? Streak { get; set; }
        public int? CurrentStreak { get; set; }
        public int? LongestStreak { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public int? Coins { get; set; }
        public int? GoalsId { get; set; }
    }
}