using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Implementation.Interfaces;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Implementation.Services
{
    public class LessonQuizResultService : ILessonQuizResultService
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public LessonQuizResultService(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<LessonsQuizResultDto>>> GetAllResultsAsync()
        {
            var results = await _context.LessonQuizResults.ToListAsync();
            var dtos = _mapper.Map<List<LessonsQuizResultDto>>(results);
            return new OkObjectResult(dtos);
        }

        public async Task<ActionResult<LessonsQuizResultDto>> GetResultAsync(int personId, int lessonQuizId)
        {
            var entity = await _context.LessonQuizResults.FindAsync(personId, lessonQuizId);
            if (entity == null)
                return new NotFoundObjectResult($"Result for person {personId}, quiz {lessonQuizId} not found.");

            var dto = _mapper.Map<LessonsQuizResultDto>(entity);
            return new OkObjectResult(dto);
        }

        public async Task<ActionResult<LessonsQuizResultDto>> CreateResultAsync(CreateLessonsQuizResultDto dto)
        {
            var entity = new LessonQuizResult
            {
                PersonId = dto.PersonId,
                LessonQuizId = dto.LessonQuizId,
                Score = dto.Score,
                IsCompleted = dto.IsCompleted,
                CompletedAt = dto.IsCompleted ? DateTime.UtcNow : (DateTime?)null
            };

            _context.LessonQuizResults.Add(entity);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<LessonsQuizResultDto>(entity);
            return new CreatedAtActionResult(
                actionName: nameof(Controllers.LessonsQuizResultController.Get),
                controllerName: "LessonsQuizResult",
                routeValues: new { personId = entity.PersonId, lessonQuizId = entity.LessonQuizId },
                value: resultDto
            );
        }

        public async Task<ActionResult> UpdateResultAsync(string personId, int lessonQuizId, LessonsQuizResultDto dto)
        {
            if (dto == null)
                return new BadRequestObjectResult("Payload cannot be null!");

            if (personId != dto.PersonId || lessonQuizId != dto.LessonQuizId)
                return new BadRequestObjectResult("Route IDs must match payload.");

            var entity = await _context.LessonQuizResults.FindAsync(personId, lessonQuizId);
            if (entity == null)
                return new NotFoundObjectResult($"Result for person {personId}, quiz {lessonQuizId} not found.");

            entity.Score = dto.Score;
            entity.CompletedAt = dto.CompletedAt;

            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        public async Task<ActionResult> DeleteResultAsync(string personId, int lessonQuizId)
        {
            var entity = await _context.LessonQuizResults.FindAsync(personId, lessonQuizId);
            if (entity == null)
                return new NotFoundObjectResult($"Result for person {personId}, quiz {lessonQuizId} not found.");

            _context.LessonQuizResults.Remove(entity);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }
    }
}
