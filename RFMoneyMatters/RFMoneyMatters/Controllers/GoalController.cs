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
    public class GoalController : ControllerBase
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public GoalController(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoalDto>>> GetGoals()
        {
            var goals = await _context.Goals.ToListAsync();

            var dtoList = _mapper.Map<List<GoalDto>>(goals);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GoalDto>> GetGoal(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal == null) return NotFound();
            return Ok(_mapper.Map<GoalDto>(goal));
        }

        [HttpPost]
        public async Task<ActionResult<GoalDto>> PostGoal(CreateGoalDto createDto)
        {
            var goal = _mapper.Map<Goal>(createDto);
            goal.IsSet = false;
            goal.ProgressPercentage = 0;
            goal.CompletedDate = null;

            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();

            var goalDto = _mapper.Map<GoalDto>(goal);
            return CreatedAtAction(nameof(GetGoal), new { id = goal.Id }, goalDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal == null) return NotFound();

            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
