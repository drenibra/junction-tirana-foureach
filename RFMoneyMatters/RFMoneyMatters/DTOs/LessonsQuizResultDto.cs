using RFMoneyMatters.Models;

namespace RFMoneyMatters.DTOs
{
    public class LessonsQuizResultDto
    {
        public string PersonId { get; set; }
        public int LessonQuizId { get; set; }
        public int Score { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

    public class CreateLessonsQuizResultDto
    {
        public string PersonId { get; set; }
        public int LessonQuizId { get; set; }
        public int Score { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}