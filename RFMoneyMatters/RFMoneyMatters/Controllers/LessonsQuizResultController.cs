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
    public class LessonsQuizResultController : ControllerBase
    {
        private readonly ILessonQuizResultService _resultService;

        public LessonsQuizResultController(ILessonQuizResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonsQuizResultDto>>> GetAll()
            => await _resultService.GetAllResultsAsync();

        [HttpGet("{personId}/{lessonQuizId}")]
        public async Task<ActionResult<LessonsQuizResultDto>> Get(int personId, int lessonQuizId)
            => await _resultService.GetResultAsync(personId, lessonQuizId);

        [HttpPost]
        public async Task<ActionResult<LessonsQuizResultDto>> Create(CreateLessonsQuizResultDto dto)
            => await _resultService.CreateResultAsync(dto);

        [HttpPut("{personId}/{lessonQuizId}")]
        public async Task<IActionResult> Update(
            string personId,
            int lessonQuizId,
            LessonsQuizResultDto dto)
            => await _resultService.UpdateResultAsync(personId, lessonQuizId, dto);

        [HttpDelete("{personId}/{lessonQuizId}")]
        public async Task<IActionResult> Delete(string personId, int lessonQuizId)
            => await _resultService.DeleteResultAsync(personId, lessonQuizId);
    }
}
