namespace RFMoneyMatters.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public int DifficultyLevel { get; set; }
        public LessonQuiz Quiz { get; set; }
    }
}