using ExpenseManagerDataAccesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagerDataAccesLibrary.Interfaces
{
    public interface IExpenseRepository  
    {
        IEnumerable<Expenses> GetAllExpensesByUser(string userId, DateTime? dateFrom, DateTime? dateTo, int? categoryId, string sort);
        void UpdateExpense(Expenses expenses);
        void AddExpense(Expenses expenses);
        void RemoveExpense(int id);
        void AddExpenseCategory(ExpenseCategories expenseCategories);

    }
}
