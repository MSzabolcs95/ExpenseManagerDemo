using ExpenseManagerDataAccesLibrary.Models;
using ExpenseManagerDataAccesLibrary.Repositories;
using ExpenseManagerDemo.Models;
using ExpenseManagerDataAccesLibrary.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseManagerDemo.Controllers
{
    public class IncomeController : Controller
    {
        private readonly IncomeRepository incomeRepository = new IncomeRepository();

        // GET: Income
        [Authorize]
        public ActionResult Index(string sort, DateTime? dateFrom, DateTime? dateTo, int? categoryId)
        {
            if (dateFrom != null && dateTo != null)
            {
                ViewBag.DateFrom = dateFrom;
                ViewBag.DateTo = dateTo;
            }
            ViewBag.Sort = "desc";
            ViewBag.Sort = !String.IsNullOrEmpty(sort) && sort == "asc" ? "desc" : "asc";
            ViewBag.IncomeCategories = IncomeRepository.Categories;

            IEnumerable<Incomes> incomes = incomeRepository.GetAllIncomesByUser(User.Identity.GetUserId(), dateFrom, dateTo, categoryId, sort);
            List<IncomeViewModel> incomeViewModel = new List<IncomeViewModel>();
            foreach (var item in incomes)
            {
                var viewModelItem = new IncomeViewModel
                {
                    Id = item.Id,
                    Category = incomeRepository.GetCategoryNameById(item.CategoryId),
                    Date = item.Date,
                    Name = item.Name,
                    Value = item.Value
                };

                incomeViewModel.Add(viewModelItem);
            }
            return View(incomeViewModel);
        }

        [Authorize]
        public ActionResult AddIncome()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddIncome(Incomes incomes)
        {
            incomes.UserId = User.Identity.GetUserId();
            incomeRepository.AddIncome(incomes);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult EditIncome(int id)
        {
            var income = incomeRepository.GetIncomeById(id);

            return View(income);

        }
        [HttpPost]
        [Authorize]
        public ActionResult EditIncome(Incomes incomes)
        {
            incomes.UserId = User.Identity.GetUserId();
            incomeRepository.UpdateIncome(incomes);

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult AddCategory()
        {
            return View();
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            incomeRepository.RemoveIncome(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddCategory(IncomeCategories incomeCategories)
        {
            incomeRepository.AddIncomeCategory(incomeCategories);
            return RedirectToAction("AddIncome");
        }
    }
}