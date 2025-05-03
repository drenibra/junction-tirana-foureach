using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserChallengeController : ControllerBase
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public UserChallengeController(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("user/{personId}")]
        public async Task<ActionResult<List<UserChallengeDto>>> GetUserChallenges(int personId)
        {
            var userChallenges = await _context.UserChallenges
                .Where(x => x.PersonId.Equals(personId))
                .ToListAsync();

            return Ok(_mapper.Map<List<UserChallengeDto>>(userChallenges));
        }

        [HttpPost]
        public async Task<ActionResult> AssignUserChallenge(CreateUserChallengeDto dto)
        {
            var userChallenge = _mapper.Map<UserChallenge>(dto);
            await _context.UserChallenges.AddAsync(userChallenge);
            await _context.SaveChangesAsync();
            return Ok("User challenge assigned successfully");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateUserChallenge(int id, UpdateUserChallengeStatusDto dto)
        {
            var challenge = await _context.UserChallenges.FindAsync(id);
            if (challenge == null)
                return NotFound("User challenge not found");

            if (dto.IsCompleted.HasValue)
                challenge.IsCompleted = dto.IsCompleted.Value;

            if (dto.CompletedDate.HasValue)
                challenge.CompletedDate = dto.CompletedDate.Value;

            await _context.SaveChangesAsync();
            return Ok("User challenge status updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserChallenge(int id)
        {
            var challenge = await _context.UserChallenges.FindAsync(id);
            if (challenge == null)
                return NotFound("User challenge not found");

            _context.UserChallenges.Remove(challenge);
            await _context.SaveChangesAsync();
            return Ok("User challenge deleted");
        }
    }
}
