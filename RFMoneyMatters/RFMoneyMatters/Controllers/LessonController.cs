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
    public class LessonController : ControllerBase
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public LessonController(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons()
        {
            var lessons = await _context.Lessons
                .ToListAsync();

            var dtoList = _mapper.Map<List<LessonDto>>(lessons);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetLesson(int id)
        {
            var lesson = await _context.Lessons
                .Include(l => l.Quiz)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lesson == null)
                return NotFound();

            return Ok(_mapper.Map<LessonDto>(lesson));
        }

        [HttpPost]
        public async Task<ActionResult<LessonDto>> PostLesson(CreateLessonDto createDto)
        {
            var lesson = _mapper.Map<Lesson>(createDto);

            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<LessonDto>(lesson);
            return CreatedAtAction(nameof(GetLesson),
                                   new { id = lesson.Id },
                                   resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id, UpdateLessonDto dto)
        {
            if (dto == null)
                return BadRequest("Payload cannot be null!");

            var lesson = await _context.Lessons
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lesson == null)
                return NotFound($"Lesson {id} not found.");

            lesson.Title = dto.Title ?? lesson.Title;
            lesson.Type = dto.Type ?? lesson.Type;
            lesson.Content = dto.Content ?? lesson.Content;
            lesson.DifficultyLevel = dto.DifficultyLevel ?? lesson.DifficultyLevel;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
                return NotFound();

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}