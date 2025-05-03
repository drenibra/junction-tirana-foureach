using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Implementation.Interfaces;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Implementation.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public ChallengeService(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<ChallengeDefinitionDto>>> GetAllChallengesAsync()
        {
            var challenges = await _context.ChallengeDefinitions.ToListAsync();
            var dtos = _mapper.Map<List<ChallengeDefinitionDto>>(challenges);
            return new OkObjectResult(dtos);
        }

        public async Task<ActionResult<ChallengeDefinitionDto>> GetChallengeByIdAsync(int id)
        {
            var challenge = await _context.ChallengeDefinitions.FindAsync(id);
            if (challenge == null)
                return new NotFoundObjectResult("Challenge not found");

            var dto = _mapper.Map<ChallengeDefinitionDto>(challenge);
            return new OkObjectResult(dto);
        }

        public async Task<ActionResult> CreateChallengeAsync(CreateChallengeDefinitionDto dto)
        {
            var entity = _mapper.Map<ChallengeDefinition>(dto);
            entity.ChallengeDate = DateTime.SpecifyKind(entity.ChallengeDate, DateTimeKind.Utc);

            await _context.ChallengeDefinitions.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new OkObjectResult("Challenge successfully created");
        }

        public async Task<ActionResult> UpdateChallengeAsync(int id, UpdateChallengeDefinitionDto dto)
        {
            var challenge = await _context.ChallengeDefinitions.FindAsync(id);
            if (challenge == null)
                return new NotFoundObjectResult("Challenge not found");

            challenge.Title = dto.Title ?? challenge.Title;
            challenge.Category = dto.Category ?? challenge.Category;
            challenge.Description = dto.Description ?? challenge.Description;
            challenge.ChallengeDate = dto.ChallengeDate.HasValue
                                        ? DateTime.SpecifyKind(dto.ChallengeDate.Value, DateTimeKind.Utc)
                                        : challenge.ChallengeDate;

            await _context.SaveChangesAsync();
            return new OkObjectResult("Challenge successfully updated");
        }

        public async Task<ActionResult> DeleteChallengeAsync(int id)
        {
            var challenge = await _context.ChallengeDefinitions.FindAsync(id);
            if (challenge == null)
                return new NotFoundObjectResult("Challenge not found");

            _context.ChallengeDefinitions.Remove(challenge);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Challenge successfully deleted");
        }
    }
}
