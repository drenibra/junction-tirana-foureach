namespace RFMoneyMatters.DTOs
{
    public class ChallengeDefinitionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime ChallengeDate { get; set; }
    }

    public class CreateChallengeDefinitionDto
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime ChallengeDate { get; set; }
    }

    public class UpdateChallengeDefinitionDto
    {
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public DateTime? ChallengeDate { get; set; }
    }
}
