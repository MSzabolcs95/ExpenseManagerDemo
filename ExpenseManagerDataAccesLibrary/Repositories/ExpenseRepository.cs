using ExpenseManagerDataAccesLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManagerDataAccesLibrary.Models;

namespace ExpenseManagerDataAccesLibrary.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private AppDbContext context = null;

        public static volatile List<ExpenseCategories> Categories;

        public ExpenseRepository()
        {
            context = new AppDbContext();
            Categories = context.ExpenseCategories.ToList();
        }

        public void AddExpense(Expenses expenses)
        {
            context.Expenses.Add(expenses);
            context.SaveChanges();
        }

        public IEnumerable<Expenses> GetAllExpensesByUser(string userId, DateTime? dateFrom, DateTime? dateTo, int? categoryId, string sort)
        {
            if (dateFrom == null && dateTo == null)
            {
                dateFrom = DateTime.MinValue;
                dateTo = DateTime.MaxValue;
            }
            if (categoryId == null)
            {
                if(sort != null)
                {
                    if (sort == "desc")
                    {
                        return context.Expenses.Where(x => x.UserId == userId && x.Date > dateFrom && x.Date < dateTo && x.IsDeleted != true).ToList().OrderByDescending(x => x.Value);
                    }
                    else
                    {
                        return context.Expenses.Where(x => x.UserId == userId && x.Date > dateFrom && x.Date < dateTo && x.IsDeleted != true).ToList().OrderBy(x => x.Value);
                    }
                }
                else
                {
                    return context.Expenses.Where(x => x.UserId == userId && x.Date > dateFrom && x.Date < dateTo && x.IsDeleted != true).ToList();
                }
                
            }
            else
            {
                if (sort != null)
                {
                    if (sort == "desc")
                    {
                        return context.Expenses.Where(x => x.UserId == userId && x.Date > dateFrom && x.Date < dateTo && x.CategoryId == categoryId && x.IsDeleted != true).ToList().OrderByDescending(x => x.Value);
                    }
                    else
                    {
                        return context.Expenses.Where(x => x.UserId == userId && x.Date > dateFrom && x.Date < dateTo && x.CategoryId == categoryId && x.IsDeleted != true).ToList().OrderBy(x => x.Value);
                    }
                }
                else
                {
                    return context.Expenses.Where(x => x.UserId == userId && x.Date > dateFrom && x.Date < dateTo && x.CategoryId == categoryId && x.IsDeleted != true).ToList();
                }
            }
           
        }

        public void RemoveExpense(int id)
        {
            var removeExpense = context.Expenses.Where(x => x.Id == id).FirstOrDefault();

            removeExpense.IsDeleted = true;

            context.SaveChanges();
        }

        public Expenses GetExpenseById(int id)
        {
            return context.Expenses.Where(x => x.Id == id).FirstOrDefault();
        }

        public string GetCategoryNameById(int id)
        {
            return context.ExpenseCategories.Where(x => x.Id == id).FirstOrDefault().CategoryName;
        }

        public void UpdateExpense(Expenses expenses)
        {
            var updateExpense = context.Expenses.Where(x => x.Id == expenses.Id).FirstOrDefault();

            updateExpense.Name = expenses.Name;
            updateExpense.UserId = expenses.UserId;
            updateExpense.Value = expenses.Value;
            updateExpense.Date = expenses.Date;

            context.SaveChanges();
        }

        public void AddExpenseCategory(ExpenseCategories expenseCategories)
        {
            context.ExpenseCategories.Add(expenseCategories);
            context.SaveChanges();
        }
    }
}
