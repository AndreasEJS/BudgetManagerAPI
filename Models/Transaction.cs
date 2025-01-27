using System;

namespace BudgetManagerAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; } // Unikt ID för varje transaktion
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set;  }
        public int CategoryId { get; set; } // ID för associerad kategori
        public Category category { get; set; } // Navigeringsproperty för kategori 








    }
}
