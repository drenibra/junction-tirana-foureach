namespace RFMoneyMatters.Models
{
    public class Questionnaire
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }

        public ICollection<Question> Questions { get; set; }

    }
}
