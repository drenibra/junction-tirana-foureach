using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Models;
using System.Collections.Generic;

namespace RFMoneyMatters.Configurations
{
    public class RaiDbContext : IdentityDbContext<Person>
    {
        public RaiDbContext(DbContextOptions<RaiDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ChallengeDefinition> ChallengeDefinitions { get; set; }
        public DbSet<UserChallenge> UserChallenges { get; set; }
        public DbSet<LessonQuiz> LessonQuizzes { get; set; }
        public DbSet<LessonQuizQuestion> LessonQuizQuestions { get; set; }
        public DbSet<LessonQuizResult> LessonQuizResults { get; set; }
        public DbSet<Expense> Expenses { get; set; }

    }
}
