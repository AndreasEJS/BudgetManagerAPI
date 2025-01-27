namespace BudgetManagerAPI.Models
{
    public class TransactionDto
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Date { get; set; }
        public int CategoryId { get; set; }
    }
}
