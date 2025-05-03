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
    public class ChallengeController : ControllerBase
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public ChallengeController(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChallengeDefinitionDto>>> GetAllChallenges()
        {
            var challenges = await _context.ChallengeDefinitions.ToListAsync();
            return Ok(_mapper.Map<List<ChallengeDefinitionDto>>(challenges));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChallengeDefinitionDto>> GetChallenge(int id)
        {
            var challenge = await _context.ChallengeDefinitions.FindAsync(id);
            if (challenge == null)
                return NotFound("Challenge not found");

            return Ok(_mapper.Map<ChallengeDefinitionDto>(challenge));
        }

        [HttpPost]
        public async Task<ActionResult> CreateChallenge(CreateChallengeDefinitionDto dto)
        {
            var challenge = _mapper.Map<ChallengeDefinition>(dto);
            challenge.ChallengeDate = DateTime.SpecifyKind(challenge.ChallengeDate, DateTimeKind.Utc);
            await _context.ChallengeDefinitions.AddAsync(challenge);
            await _context.SaveChangesAsync();

            return Ok("Challenge successfully created");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateChallenge(int id, UpdateChallengeDefinitionDto dto)
        {
            var challenge = await _context.ChallengeDefinitions.FindAsync(id);
            if (challenge == null)
                return NotFound("Challenge not found");

            challenge.Title = dto.Title ?? challenge.Title;
            challenge.Category = dto.Category ?? challenge.Category;
            challenge.Description = dto.Description ?? challenge.Description;
            challenge.ChallengeDate = dto.ChallengeDate ?? challenge.ChallengeDate;

            await _context.SaveChangesAsync();
            return Ok("Challenge successfully updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChallenge(int id)
        {
            var challenge = await _context.ChallengeDefinitions.FindAsync(id);
            if (challenge == null)
                return NotFound("Challenge not found");

            _context.ChallengeDefinitions.Remove(challenge);
            await _context.SaveChangesAsync();
            return Ok("Challenge successfully deleted");
        }
    }
}
