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
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons()
            => await _lessonService.GetLessonsAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetLesson(int id)
            => await _lessonService.GetLessonByIdAsync(id);

        [HttpPost]
        public async Task<ActionResult<LessonDto>> PostLesson(CreateLessonDto createDto)
            => await _lessonService.CreateLessonAsync(createDto);

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id, UpdateLessonDto dto)
            => await _lessonService.UpdateLessonAsync(id, dto);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
            => await _lessonService.DeleteLessonAsync(id);
    }
}