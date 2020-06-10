using ExpenseManagerDataAccesLibrary;
using ExpenseManagerDataAccesLibrary.Interfaces;
using ExpenseManagerDataAccesLibrary.Models;
using ExpenseManagerDataAccesLibrary.Repositories;
using ExpenseManagerDemo.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseManagerDemo.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ExpenseRepository expenseRepository = new ExpenseRepository();

        // GET: Expense
        [Authorize]
        public ActionResult Index(string sort, DateTime? dateFrom, DateTime? dateTo, int? categoryId)
        {
            if(dateFrom != null && dateTo != null)
            {
                ViewBag.DateFrom = dateFrom;
                ViewBag.DateTo = dateTo;
            }
            ViewBag.Sort = "desc";
            ViewBag.Sort = !String.IsNullOrEmpty(sort)&& sort=="asc" ? "desc" : "asc";
            ViewBag.ExpenseCategories = ExpenseRepository.Categories;

            IEnumerable<Expenses> expenses = expenseRepository.GetAllExpensesByUser(User.Identity.GetUserId(), dateFrom, dateTo, categoryId, sort);
            List<ExpenseViewModel> expenseViewModel = new List<ExpenseViewModel>();
            foreach(var item in expenses)
            {
                var viewModelItem = new ExpenseViewModel
                {
                    Id = item.Id,
                    Category = expenseRepository.GetCategoryNameById(item.CategoryId),
                    Date = item.Date,
                    Name = item.Name,
                    Value = item.Value
                };

                expenseViewModel.Add(viewModelItem);
            }
            return View(expenseViewModel);
        }

        [Authorize]
        public ActionResult AddExpense()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddExpense(Expenses expenses)
        {
            expenses.UserId = User.Identity.GetUserId();
            expenseRepository.AddExpense(expenses);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            expenseRepository.RemoveExpense(id);

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult EditExpense (int id)
        {
            var expense = expenseRepository.GetExpenseById(id);

            return View(expense);

        }
        [HttpPost]
        [Authorize]
        public ActionResult EditExpense(Expenses expenses)
        {
            expenses.UserId = User.Identity.GetUserId();
            expenseRepository.UpdateExpense(expenses);

            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddCategory (ExpenseCategories expenseCategories)
        {
            expenseRepository.AddExpenseCategory(expenseCategories);
            return RedirectToAction("AddExpense");
        }
    }
}