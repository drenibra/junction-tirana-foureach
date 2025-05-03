using RFMoneyMatters.Models;

namespace RFMoneyMatters.DTOs
{
    public class LessonQuizQuestionDto
    {
        public string Title { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string? C { get; set; }
        public string? D { get; set; }
        public string RightAnswer { get; set; }
    }

    public class CreateLessonQuizQuestionDto
    {
        public string Title { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string? C { get; set; }
        public string? D { get; set; }
        public string RightAnswer { get; set; }
    }

    public class UpdateLessonQuizQuestionDto
    {
        public string? Title { get; set; }
        public string? A { get; set; }
        public string? B { get; set; }
        public string? C { get; set; }
        public string? D { get; set; }
        public string? RightAnswer { get; set; }
    }
}