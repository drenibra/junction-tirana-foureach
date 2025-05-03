using Microsoft.AspNetCore.Mvc;
using RFMoneyMatters.DTOs;

namespace RFMoneyMatters.Implementation.Interfaces
{
    public interface IUserChallengeService
    {
        Task<ActionResult<List<UserChallengeDto>>> GetUserChallengesAsync(string personId);
        Task<ActionResult> AssignUserChallengeAsync(CreateUserChallengeDto dto);
        Task<ActionResult> UpdateUserChallengeStatusAsync(int id, UpdateUserChallengeStatusDto dto);
        Task<ActionResult> DeleteUserChallengeAsync(int id);
    }
}
