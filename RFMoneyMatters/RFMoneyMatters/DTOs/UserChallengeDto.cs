namespace RFMoneyMatters.DTOs
{
    public class UserChallengeDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ChallengeDefinitionId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? CompletedDate { get; set; }

    }

    public class CreateUserChallengeDto
    {
        public int PersonId { get; set; }
        public int ChallengeDefinitionId { get; set; }
        public DateTime AssignedDate { get; set; }
    }

    public class UpdateUserChallengeStatusDto
    {
        public bool? IsCompleted { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
