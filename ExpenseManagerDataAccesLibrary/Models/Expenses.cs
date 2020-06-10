using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagerDataAccesLibrary.Models
{
    public class Expenses
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
