using RFMoneyMatters.Models;

namespace RFMoneyMatters.DTOs
{
    public class LessonDto
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public int DifficultyLevel { get; set; }
        public LessonQuiz? Quiz { get; set; }
    }

    public class CreateLessonDto
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public int DifficultyLevel { get; set; }
    }

    public class UpdateLessonDto
    {
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Content { get; set; }
        public int? DifficultyLevel { get; set; }
    }
}