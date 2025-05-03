using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Implementation.Interfaces;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Implementation.Services
{
    public class GoalService : IGoalService
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public GoalService(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<GoalDto>>> GetGoalsAsync()
        {
            var goals = await _context.Goals.ToListAsync();
            return _mapper.Map<List<GoalDto>>(goals);
        }

        public async Task<ActionResult<GoalDto>> GetGoalByIdAsync(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
                return new NotFoundResult();

            return _mapper.Map<GoalDto>(goal);
        }

        public async Task<ActionResult<GoalDto>> CreateGoalAsync(CreateGoalDto createDto)
        {
            var goal = _mapper.Map<Goal>(createDto);
            goal.IsSet = false;
            goal.ProgressPercentage = 0;
            goal.CompletedDate = null;

            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<GoalDto>(goal);
            return new CreatedAtActionResult(
                actionName: nameof(GetGoalByIdAsync),
                controllerName: "Goal",
                routeValues: new { id = goal.Id },
                value: dto
            );
        }

        public async Task<ActionResult> DeleteGoalAsync(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
                return new NotFoundResult();

            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }
    }
}