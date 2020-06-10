using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseManagerDemo.Models
{
    public class IncomeViewModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}