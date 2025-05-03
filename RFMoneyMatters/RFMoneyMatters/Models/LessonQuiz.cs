namespace RFMoneyMatters.Models
{
    public class LessonQuiz
    {
        public int Id { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public ICollection<LessonQuizQuestion> Questions { get; set; }
    }
}