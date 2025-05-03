namespace RFMoneyMatters.DTOs
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
    }

    public class CreateExpenseDto
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string PersonId { get; set; }
    }

}
