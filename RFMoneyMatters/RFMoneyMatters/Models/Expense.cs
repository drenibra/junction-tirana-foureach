namespace RFMoneyMatters.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public ExpenseCategory Category { get; set; }

        public string PersonId { get; set; }
        public Person Person { get; set; }
    }

    public enum ExpenseCategory
    {
        Need = 1,
        Want = 2
    }
}
