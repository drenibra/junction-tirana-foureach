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
            CreateMap<Person, CreatePersonDto>().ReverseMap();

            CreateMap<Goal, GoalDto>().ReverseMap();
            CreateMap<Goal, UpdateGoalDto>().ReverseMap();
            CreateMap<Goal, CreateGoalDto>().ReverseMap();

            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<Lesson, UpdateLessonDto>().ReverseMap();
            CreateMap<Lesson, CreateLessonDto>().ReverseMap();

            CreateMap<LessonQuiz, LessonQuizDto>().ReverseMap();
            CreateMap<LessonQuiz, UpdateLessonQuizDto>().ReverseMap();
            CreateMap<LessonQuiz, CreateLessonQuizDto>().ReverseMap();

            CreateMap<LessonQuizQuestion, LessonQuizQuestionDto>().ReverseMap();
            CreateMap<LessonQuizQuestion, LessonQuizQuestionDto>().ReverseMap();
            CreateMap<LessonQuizQuestion, LessonQuizQuestionDto>().ReverseMap();

            CreateMap<LessonQuizResult, LessonsQuizResultDto>().ReverseMap();
            CreateMap<LessonQuizResult, CreateLessonsQuizResultDto>().ReverseMap();

            CreateMap<ChallengeDefinition, ChallengeDefinitionDto>().ReverseMap();
            CreateMap<ChallengeDefinition, CreateChallengeDefinitionDto>().ReverseMap();
            CreateMap<ChallengeDefinition, UpdateChallengeDefinitionDto>().ReverseMap();

            CreateMap<UserChallenge, UserChallengeDto>().ReverseMap();
            CreateMap<UserChallenge, CreateUserChallengeDto>().ReverseMap();
            CreateMap<UserChallenge, UpdateUserChallengeStatusDto>().ReverseMap();

            CreateMap<Expense, ExpenseDto>().ReverseMap();
            CreateMap<Expense, CreateExpenseDto>().ReverseMap();

        }
    }
}