using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using ExpenseManagerDataAccesLibrary.Models;

namespace ExpenseManagerDataAccesLibrary
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Incomes> Incomes { get; set; }
        public DbSet<ExpenseCategories> ExpenseCategories { get; set; }
        public DbSet<IncomeCategories> IncomeCategories { get; set; }

       
    }
}
