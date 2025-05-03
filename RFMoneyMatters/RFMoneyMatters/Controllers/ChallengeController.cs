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
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeService _challengeService;

        public ChallengeController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChallengeDefinitionDto>>> GetAllChallenges()
            => await _challengeService.GetAllChallengesAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<ChallengeDefinitionDto>> GetChallenge(int id)
            => await _challengeService.GetChallengeByIdAsync(id);

        [HttpPost]
        public async Task<ActionResult> CreateChallenge(CreateChallengeDefinitionDto dto)
            => await _challengeService.CreateChallengeAsync(dto);

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateChallenge(int id, UpdateChallengeDefinitionDto dto)
            => await _challengeService.UpdateChallengeAsync(id, dto);

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChallenge(int id)
            => await _challengeService.DeleteChallengeAsync(id);
    }
}
