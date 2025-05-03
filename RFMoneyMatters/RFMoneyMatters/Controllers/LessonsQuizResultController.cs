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
    public class LessonsQuizResultController : ControllerBase
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public LessonsQuizResultController(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonsQuizResultDto>>> GetAll()
        {
            var results = await _context.LessonQuizResults.ToListAsync();
            return Ok(_mapper.Map<List<LessonsQuizResultDto>>(results));
        }

        [HttpGet("{personId}/{lessonQuizId}")]
        public async Task<ActionResult<LessonsQuizResultDto>> Get(int personId, int lessonQuizId)
        {
            var result = await _context.LessonQuizResults
                .FindAsync(personId, lessonQuizId);
            if (result == null)
                return NotFound($"Result for person {personId}, quiz {lessonQuizId} not found.");

            return Ok(_mapper.Map<LessonsQuizResultDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult<LessonsQuizResultDto>> Create(CreateLessonsQuizResultDto dto)
        {
            var entity = new LessonQuizResult
            {
                PersonId = dto.PersonId,
                LessonQuizId = dto.LessonQuizId,
                Score = dto.Score,
                IsCompleted = dto.IsCompleted,
                CompletedAt = dto.IsCompleted ? DateTime.UtcNow : null
            };

            _context.LessonQuizResults.Add(entity);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<LessonsQuizResultDto>(entity);
            return CreatedAtAction(
                nameof(Get),
                new { personId = entity.PersonId, lessonQuizId = entity.LessonQuizId },
                resultDto
            );
        }

        [HttpPut("{personId}/{lessonQuizId}")]
        public async Task<IActionResult> Update(
            int personId,
            int lessonQuizId,
            LessonsQuizResultDto dto)
        {
            if (dto == null)
                return BadRequest("Payload cannot be null!");

            if (!personId.Equals(dto.PersonId) || lessonQuizId != dto.LessonQuizId)
                return BadRequest("Route IDs must match payload.");

            var entity = await _context.LessonQuizResults
                                       .FindAsync(personId, lessonQuizId);
            if (entity == null)
                return NotFound($"Result for person {personId}, quiz {lessonQuizId} not found.");

            entity.Score = dto.Score;
            entity.CompletedAt = dto.CompletedAt;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{personId}/{lessonQuizId}")]
        public async Task<IActionResult> Delete(int personId, int lessonQuizId)
        {
            var entity = await _context.LessonQuizResults
                                       .FindAsync(personId, lessonQuizId);
            if (entity == null)
                return NotFound($"Result for person {personId}, quiz {lessonQuizId} not found.");

            _context.LessonQuizResults.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
