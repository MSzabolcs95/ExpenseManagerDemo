using ExpenseManagerDataAccesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagerDataAccesLibrary.Interfaces
{
    public interface IIncomeRepository
    {
        IEnumerable<Incomes> GetAllIncomesByUser(string userId, DateTime? dateFrom, DateTime? dateTo, int? categoryId, string sort);
        void UpdateIncome(Incomes incomes);
        void AddIncome(Incomes incomes);
        void RemoveIncome(int id);
    }
}
