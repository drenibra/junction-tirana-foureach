namespace RFMoneyMatters.Models
{
    public class LessonQuizResult
    {
        public int Id { get; set; }
        public string PersonId { get; set; }
        public Person Person { get; set; }
        public int LessonQuizId { get; set; }
        public LessonQuiz LessonQuiz { get; set; }
        public int Score { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}