namespace RFMoneyMatters.Models
{
    public class ChallengeDefinition
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; } // e.g. Budgeting, Saving
        public string Description { get; set; }
        public DateOnly ChallengeDate { get; set; }

    }
}
