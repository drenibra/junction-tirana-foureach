using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class UserChallengeController : ControllerBase
    {
        private readonly IUserChallengeService _service;

        public UserChallengeController(IUserChallengeService service)
        {
            _service = service;
        }

        [HttpGet("user/{personId}")]
        public async Task<ActionResult<List<UserChallengeDto>>> GetUserChallenges(string personId)
            => await _service.GetUserChallengesAsync(personId);

        [HttpPost]
        public async Task<ActionResult> AssignUserChallenge(CreateUserChallengeDto dto)
            => await _service.AssignUserChallengeAsync(dto);

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateUserChallenge(int id, UpdateUserChallengeStatusDto dto)
            => await _service.UpdateUserChallengeStatusAsync(id, dto);

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserChallenge(int id)
            => await _service.DeleteUserChallengeAsync(id);
    }
}
