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
        }
    }
}