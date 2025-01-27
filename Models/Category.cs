using System.Collections.Generic;

namespace BudgetManagerAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public String Name { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public List<Transaction> Transactions { get; set; }



    }
}
