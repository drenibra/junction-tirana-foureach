using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Implementation.Interfaces;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Implementation.Services
{
    public class LessonQuizService : ILessonQuizService
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public LessonQuizService(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<LessonQuizDto>>> GetAllQuizzesAsync()
        {
            var quizzes = await _context.LessonQuizzes
                .Include(q => q.Questions)
                .ToListAsync();

            var dtos = _mapper.Map<List<LessonQuizDto>>(quizzes);
            return new OkObjectResult(dtos);
        }

        public async Task<ActionResult<LessonQuizDto>> GetQuizByLessonIdAsync(int lessonId)
        {
            var quiz = await _context.LessonQuizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.LessonId == lessonId);

            if (quiz == null)
                return new NotFoundObjectResult($"Quiz for Lesson {lessonId} not found.");

            var dto = _mapper.Map<LessonQuizDto>(quiz);
            return new OkObjectResult(dto);
        }

        public async Task<ActionResult<LessonQuizDto>> CreateQuizAsync(CreateLessonQuizDto createDto)
        {
            // Validate that the Lesson exists
            var exists = await _context.Lessons.AnyAsync(l => l.Id == createDto.LessonId);
            if (!exists)
                return new BadRequestObjectResult($"Lesson {createDto.LessonId} does not exist.");

            var quiz = _mapper.Map<LessonQuiz>(createDto);

            _context.LessonQuizzes.Add(quiz);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<LessonQuizDto>(quiz);
            return new CreatedAtActionResult(
                actionName: nameof(Controllers.LessonQuizController.GetQuiz),
                controllerName: "LessonQuiz",
                routeValues: new { lessonId = quiz.LessonId },
                value: resultDto
            );
        }

        public async Task<ActionResult> UpdateQuizAsync(int lessonId, UpdateLessonQuizDto updateDto)
        {
            if (updateDto == null)
                return new BadRequestObjectResult("Payload cannot be null!");

            var quiz = await _context.LessonQuizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.LessonId == lessonId);

            if (quiz == null)
                return new NotFoundObjectResult($"Quiz for Lesson {lessonId} not found.");

            if (updateDto.LessonId.HasValue && updateDto.LessonId.Value != lessonId)
                return new BadRequestObjectResult("Cannot change the LessonId of an existing quiz.");

            if (updateDto.Questions != null)
            {
                // Replace questions
                _context.LessonQuizQuestions.RemoveRange(quiz.Questions);
                quiz.Questions = updateDto.Questions
                    .Select(qDto => _mapper.Map<LessonQuizQuestion>(qDto))
                    .ToList();
            }

            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        public async Task<ActionResult> DeleteQuizAsync(int lessonId)
        {
            var quiz = await _context.LessonQuizzes
                .FirstOrDefaultAsync(q => q.LessonId == lessonId);

            if (quiz == null)
                return new NotFoundObjectResult($"Quiz for Lesson {lessonId} not found.");

            _context.LessonQuizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }
    }
}
