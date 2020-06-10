using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManagerDataAccesLibrary.Models;
using ExpenseManagerDataAccesLibrary;
using ExpenseManagerDataAccesLibrary.Interfaces;

namespace ExpenseManagerDataAccesLibrary.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private AppDbContext context = null;
        public static volatile List<IncomeCategories> Categories;

        public IncomeRepository()
        {
            context = new AppDbContext();
            Categories = context.IncomeCategories.ToList();
        }

        public void AddIncome(Incomes incomes)
        {
            context.Incomes.Add(incomes);
            context.SaveChanges();
        }

        public IEnumerable<Incomes> GetAllIncomesByUser(string userId, DateTime? dateFrom, DateTime? dateTo, int? categoryId, string sort)
        {
            if (dateFrom == null && dateTo == null)
            {
                dateFrom = DateTime.MinValue;
                dateTo = DateTime.MaxValue;
            }
            if (categoryId == null)
            {
                if (sort != null)
                {
                    if (sort == "desc")
                    {
                        return context.Incomes.Where(x => x.UserId == userId && x.Date > dateFrom && x.Date < dateTo && x.IsDeleted !=true).ToList().OrderByDescending(x => x.Value);
                    }
                    else
                    {
                        return context.Incomes.Where(x => x.UserId == userId && x.Date > dateFrom && x.Date < dateTo && x.IsDeleted != true).ToList().OrderBy(x => x.Value);
                    }
                }
                else
                {
                    return context.Incomes.Where(x => x.UserId == userId && x.Date > dateFrom && x.Date < dateTo && x.IsDeleted != true).ToList();
                }

            }
            else
            {
                if (sort != null)
                {
                    if (sort == "desc")
                    {
                        return context.Incomes.Where(x => x.UserId == userId && x.Date > dateFrom && x.Date < dateTo && x.CategoryId == categoryId && x.IsDeleted != true).ToList().OrderByDescending(x => x.Value);
                    }
                    else
                    {
                        return context.Incomes.Where(x => x.UserId == userId && x.Date > dateFrom && x.Date < dateTo && x.CategoryId == categoryId && x.IsDeleted != true).ToList().OrderBy(x => x.Value);
                    }
                }
                else
                {
                    return context.Incomes.Where(x => x.UserId == userId && x.Date > dateFrom && x.Date < dateTo && x.CategoryId == categoryId && x.IsDeleted != true).ToList();
                }
            }
        }

        public void RemoveIncome(int id)
        {
            var deleteIncome = context.Incomes.Where(x => x.Id == id).FirstOrDefault();

            deleteIncome.IsDeleted = true;

            context.SaveChanges();
        }

        public string GetCategoryNameById(int id)
        {
            return context.IncomeCategories.Where(x => x.Id == id).FirstOrDefault().CategoryName;
        }

        public Incomes GetIncomeById(int id)
        {
            return context.Incomes.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateIncome(Incomes incomes)
        {
            var updateIncome = context.Incomes.Where(x => x.Id == incomes.Id).FirstOrDefault();

            updateIncome.Name = incomes.Name;
            updateIncome.UserId = incomes.UserId;
            updateIncome.Value = incomes.Value;
            updateIncome.Date = incomes.Date;

            context.SaveChanges();
        }

        public void AddIncomeCategory(IncomeCategories incomeCategories)
        {
            context.IncomeCategories.Add(incomeCategories);
            context.SaveChanges();
        }
    }
}
