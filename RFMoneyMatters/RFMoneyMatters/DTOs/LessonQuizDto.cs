using RFMoneyMatters.Models;

namespace RFMoneyMatters.DTOs
{
    public class LessonQuizDto
    {
        public int LessonId { get; set; }
        public ICollection<LessonQuizQuestionDto> Questions { get; set; }
    }

    public class CreateLessonQuizDto
    {
        public int LessonId { get; set; }
        public ICollection<LessonQuizQuestionDto> Questions { get; set; }
    }

    public class UpdateLessonQuizDto
    {
        public int? LessonId { get; set; }
        public ICollection<LessonQuizQuestionDto>? Questions { get; set; }
    }
}