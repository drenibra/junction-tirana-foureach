using AutoMapper;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Configurations
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<Person, UpdatePersonDto>().ReverseMap();
            CreateMap<Goal, GoalDto>().ReverseMap();
        }
    }
}