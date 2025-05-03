namespace RFMoneyMatters.Models
{
    public class UserLessonProgress
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}