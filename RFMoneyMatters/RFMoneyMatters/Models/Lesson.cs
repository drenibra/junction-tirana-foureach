namespace RFMoneyMatters.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; } // e.g. "Budgeting", "Savings"
        public string Content { get; set; }
        public int DifficultyLevel { get; set; }

    }
}
