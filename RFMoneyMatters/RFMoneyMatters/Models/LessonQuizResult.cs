namespace RFMoneyMatters.Models
{
    public class LessonQuizResult
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int LessonQuizId { get; set; }
        public LessonQuiz LessonQuiz { get; set; }

        public int Score { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}