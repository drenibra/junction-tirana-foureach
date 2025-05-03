using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Implementation.Interfaces;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Implementation.Services
{
    public class LessonService : ILessonService
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public LessonService(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessonsAsync()
        {
            var lessons = await _context.Lessons.ToListAsync();
            return _mapper.Map<List<LessonDto>>(lessons);
        }

        public async Task<ActionResult<LessonDto>> GetLessonByIdAsync(int id)
        {
            var lesson = await _context.Lessons
                .Include(l => l.Quiz)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lesson == null)
                return new NotFoundResult();

            return _mapper.Map<LessonDto>(lesson);
        }

        public async Task<ActionResult<LessonDto>> CreateLessonAsync(CreateLessonDto createDto)
        {
            var lesson = _mapper.Map<Lesson>(createDto);

            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<LessonDto>(lesson);
            return new CreatedAtActionResult(
                actionName: nameof(GetLessonByIdAsync),
                controllerName: "Lesson",
                routeValues: new { id = lesson.Id },
                value: dto
            );
        }

        public async Task<ActionResult> UpdateLessonAsync(int id, UpdateLessonDto updateDto)
        {
            if (updateDto == null)
                return new BadRequestObjectResult("Payload cannot be null!");

            var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);
            if (lesson == null)
                return new NotFoundObjectResult($"Lesson {id} not found.");

            lesson.Title = updateDto.Title ?? lesson.Title;
            lesson.Type = updateDto.Type ?? lesson.Type;
            lesson.Content = updateDto.Content ?? lesson.Content;
            lesson.DifficultyLevel = updateDto.DifficultyLevel ?? lesson.DifficultyLevel;

            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        public async Task<ActionResult> DeleteLessonAsync(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
                return new NotFoundResult();

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }
    }
}