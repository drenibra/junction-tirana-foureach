using AutoMapper;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Configurations
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Goal, GoalDto>().ReverseMap();
            // ChallengeDefinition
            CreateMap<ChallengeDefinition, ChallengeDefinitionDto>().ReverseMap();
            CreateMap<ChallengeDefinition, CreateChallengeDefinitionDto>().ReverseMap();
            CreateMap<ChallengeDefinition, UpdateChallengeDefinitionDto>().ReverseMap();

            // UserChallenge
            CreateMap<UserChallenge, UserChallengeDto>().ReverseMap();
            CreateMap<UserChallenge, CreateUserChallengeDto>().ReverseMap();
            CreateMap<UserChallenge, UpdateUserChallengeStatusDto>().ReverseMap();
        }
    }
}