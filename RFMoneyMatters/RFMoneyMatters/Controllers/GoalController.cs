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
    public class GoalController : ControllerBase
    {
        private readonly IGoalService _goalService;

        public GoalController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoalDto>>> GetGoals()
            => await _goalService.GetGoalsAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<GoalDto>> GetGoal(int id)
            => await _goalService.GetGoalByIdAsync(id);

        [HttpPost]
        public async Task<ActionResult<GoalDto>> PostGoal(CreateGoalDto createDto)
            => await _goalService.CreateGoalAsync(createDto);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal(int id)
            => await _goalService.DeleteGoalAsync(id);
    }
}