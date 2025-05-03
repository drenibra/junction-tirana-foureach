using Microsoft.AspNetCore.Mvc;
using RFMoneyMatters.DTOs;

namespace RFMoneyMatters.Implementation.Interfaces
{
    public interface ILessonService
    {
        Task<ActionResult<IEnumerable<LessonDto>>> GetLessonsAsync();
        Task<ActionResult<LessonDto>> GetLessonByIdAsync(int id);
        Task<ActionResult<LessonDto>> CreateLessonAsync(CreateLessonDto createDto);
        Task<ActionResult> UpdateLessonAsync(int id, UpdateLessonDto updateDto);
        Task<ActionResult> DeleteLessonAsync(int id);
    }
}