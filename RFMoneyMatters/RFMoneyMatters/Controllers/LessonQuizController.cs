using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonQuizController : ControllerBase
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public LessonQuizController(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonQuizDto>>> GetAllQuizzes()
        {
            var quizzes = await _context.LessonQuizzes
                .Include(p=> p.Questions)
                .ToListAsync();

            return Ok(_mapper.Map<List<LessonQuizDto>>(quizzes));
        }

        [HttpGet("{lessonId}")]
        public async Task<ActionResult<LessonQuizDto>> GetQuiz(int lessonId)
        {
            var quiz = await _context.LessonQuizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.LessonId == lessonId);

            if (quiz == null)
                return NotFound($"Quiz for Lesson {lessonId} not found.");

            return Ok(_mapper.Map<LessonQuizDto>(quiz));
        }

        [HttpPost]
        public async Task<ActionResult<LessonQuizDto>> CreateQuiz(CreateLessonQuizDto createDto)
        {
            if (!await _context.Lessons.AnyAsync(l => l.Id == createDto.LessonId))
                return BadRequest($"Lesson {createDto.LessonId} does not exist.");

            var quiz = _mapper.Map<LessonQuiz>(createDto);

            _context.LessonQuizzes.Add(quiz);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<LessonQuizDto>(quiz);
            return CreatedAtAction(nameof(GetQuiz),
                                   new { lessonId = quiz.LessonId },
                                   result);
        }

        [HttpPut("{lessonId}")]
        public async Task<IActionResult> UpdateQuiz(int lessonId, UpdateLessonQuizDto dto)
        {
            if (dto == null)
                return BadRequest("Payload cannot be null!");

            var quiz = await _context.LessonQuizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.LessonId == lessonId);

            if (quiz == null)
                return NotFound($"Quiz for Lesson {lessonId} not found.");

            if (dto.LessonId.HasValue && dto.LessonId.Value != lessonId)
                return BadRequest("Cannot change the LessonId of an existing quiz.");

            if (dto.Questions != null)
            {
                _context.LessonQuizQuestions
                    .RemoveRange(quiz.Questions);

                quiz.Questions = (ICollection<LessonQuizQuestion>)dto.Questions;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{lessonId}")]
        public async Task<IActionResult> DeleteQuiz(int lessonId)
        {
            var quiz = await _context.LessonQuizzes.FindAsync(lessonId);
            if (quiz == null)
                return NotFound($"Quiz for Lesson {lessonId} not found.");

            _context.LessonQuizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}