using Microsoft.AspNetCore.Mvc;
using RFMoneyMatters.DTOs;

namespace RFMoneyMatters.Implementation.Interfaces
{
    public interface ILessonQuizResultService
    {
        Task<ActionResult<IEnumerable<LessonsQuizResultDto>>> GetAllResultsAsync();
        Task<ActionResult<LessonsQuizResultDto>> GetResultAsync(int personId, int lessonQuizId);
        Task<ActionResult<LessonsQuizResultDto>> CreateResultAsync(CreateLessonsQuizResultDto dto);
        Task<ActionResult> UpdateResultAsync(string personId, int lessonQuizId, LessonsQuizResultDto dto);
        Task<ActionResult> DeleteResultAsync(string personId, int lessonQuizId);
    }
}
