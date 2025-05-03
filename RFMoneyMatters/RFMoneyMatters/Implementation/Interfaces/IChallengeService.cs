using Microsoft.AspNetCore.Mvc;
using RFMoneyMatters.DTOs;

namespace RFMoneyMatters.Implementation.Interfaces
{
    public interface IChallengeService
    {
        Task<ActionResult<List<ChallengeDefinitionDto>>> GetAllChallengesAsync();
        Task<ActionResult<ChallengeDefinitionDto>> GetChallengeByIdAsync(int id);
        Task<ActionResult> CreateChallengeAsync(CreateChallengeDefinitionDto dto);
        Task<ActionResult> UpdateChallengeAsync(int id, UpdateChallengeDefinitionDto dto);
        Task<ActionResult> DeleteChallengeAsync(int id);
    }
}
