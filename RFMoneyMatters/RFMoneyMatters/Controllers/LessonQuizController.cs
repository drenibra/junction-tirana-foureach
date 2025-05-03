using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Implementation.Interfaces;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonQuizController : ControllerBase
    {
        private readonly ILessonQuizService _quizService;

        public LessonQuizController(ILessonQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonQuizDto>>> GetAllQuizzes()
            => await _quizService.GetAllQuizzesAsync();

        [HttpGet("{lessonId}")]
        public async Task<ActionResult<LessonQuizDto>> GetQuiz(int lessonId)
            => await _quizService.GetQuizByLessonIdAsync(lessonId);

        [HttpPost]
        public async Task<ActionResult<LessonQuizDto>> CreateQuiz(CreateLessonQuizDto createDto)
            => await _quizService.CreateQuizAsync(createDto);

        [HttpPut("{lessonId}")]
        public async Task<IActionResult> UpdateQuiz(int lessonId, UpdateLessonQuizDto dto)
            => await _quizService.UpdateQuizAsync(lessonId, dto);

        [HttpDelete("{lessonId}")]
        public async Task<IActionResult> DeleteQuiz(int lessonId)
            => await _quizService.DeleteQuizAsync(lessonId);
    }
}