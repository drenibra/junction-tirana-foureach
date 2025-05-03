namespace RFMoneyMatters.Models
{
    public class ChallengeDefinition
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime ChallengeDate { get; set; }
    }
}
