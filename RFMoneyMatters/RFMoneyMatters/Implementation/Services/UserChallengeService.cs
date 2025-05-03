using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Implementation.Interfaces;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Implementation.Services
{
    public class UserChallengeService : IUserChallengeService
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public UserChallengeService(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<UserChallengeDto>>> GetUserChallengesAsync(string personId)
        {
            var entities = await _context.UserChallenges
                .Where(x => x.PersonId == personId)
                .ToListAsync();

            var dtos = _mapper.Map<List<UserChallengeDto>>(entities);
            return new OkObjectResult(dtos);
        }

        public async Task<ActionResult> AssignUserChallengeAsync(CreateUserChallengeDto dto)
        {
            var entity = _mapper.Map<UserChallenge>(dto);
            await _context.UserChallenges.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new OkObjectResult("User challenge assigned successfully");
        }

        public async Task<ActionResult> UpdateUserChallengeStatusAsync(int id, UpdateUserChallengeStatusDto dto)
        {
            var entity = await _context.UserChallenges.FindAsync(id);
            if (entity == null)
                return new NotFoundObjectResult("User challenge not found");

            if (dto.IsCompleted.HasValue)
                entity.IsCompleted = dto.IsCompleted.Value;
            if (dto.CompletedDate.HasValue)
                entity.CompletedDate = dto.CompletedDate.Value;

            await _context.SaveChangesAsync();
            return new OkObjectResult("User challenge status updated");
        }

        public async Task<ActionResult> DeleteUserChallengeAsync(int id)
        {
            var entity = await _context.UserChallenges.FindAsync(id);
            if (entity == null)
                return new NotFoundObjectResult("User challenge not found");

            _context.UserChallenges.Remove(entity);
            await _context.SaveChangesAsync();
            return new OkObjectResult("User challenge deleted");
        }
    }
}
