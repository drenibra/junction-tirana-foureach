using Microsoft.AspNetCore.Mvc;
using RFMoneyMatters.DTOs;

namespace RFMoneyMatters.Implementation.Interfaces
{
    public interface ILessonQuizService
    {
        Task<ActionResult<IEnumerable<LessonQuizDto>>> GetAllQuizzesAsync();
        Task<ActionResult<LessonQuizDto>> GetQuizByLessonIdAsync(int lessonId);
        Task<ActionResult<LessonQuizDto>> CreateQuizAsync(CreateLessonQuizDto createDto);
        Task<ActionResult> UpdateQuizAsync(int lessonId, UpdateLessonQuizDto updateDto);
        Task<ActionResult> DeleteQuizAsync(int lessonId);
    }
}
