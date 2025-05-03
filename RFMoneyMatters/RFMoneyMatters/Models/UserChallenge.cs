using System;

namespace RFMoneyMatters.Models
{
    public class UserChallenge
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int ChallengeDefinitionId { get; set; }
        public ChallengeDefinition ChallengeDefinition { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? CompletedDate { get; set; }

    }
}
