using Microsoft.AspNetCore.Mvc;
using RFMoneyMatters.DTOs;

namespace RFMoneyMatters.Implementation.Interfaces
{
    public interface IGoalService
    {
        Task<ActionResult<IEnumerable<GoalDto>>> GetGoalsAsync();
        Task<ActionResult<GoalDto>> GetGoalByIdAsync(int id);
        Task<ActionResult<GoalDto>> CreateGoalAsync(CreateGoalDto createDto);
        Task<ActionResult> DeleteGoalAsync(int id);
    }
}