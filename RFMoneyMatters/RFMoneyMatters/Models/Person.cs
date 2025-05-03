namespace RFMoneyMatters.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Streak { get; set; }
        public Goal Goals { get; set; }
    }
}