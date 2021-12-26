using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheBTeam.BLL;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.Models;

namespace TheBTeam.Web.Controllers
{
    public class BudgetDTOTemp
    {
        public IEnumerable<CategoryBudgetDto> UserBudgets { get; set; }
        public string UserId { get; set; }
    }
    public class CategoryBudgetController : Controller
    {
        private readonly PlannerContext _planerContext;

        public CategoryBudgetController(PlannerContext planerContext)
        {
            _planerContext = planerContext;
        }
        // GET: CategoryBudgetController
        //[HttpGet("Show/{id}")]
        public ActionResult Index(string id)
        {
            var modelDal = _planerContext.CategoryBudgets.Where(x => x.UserId == id).ToList();
            var model = modelDal.Select(CategoryBudgetDto.FromDal);
            
            return View(new BudgetDTOTemp(){UserId = "abc", UserBudgets = model});
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UpdateBudget(string id, IFormCollection collection)
        {
            foreach (var category in collection)
            {
                var existing = _planerContext.CategoryBudgets.AsNoTracking()
                    .FirstOrDefault(x => x.UserId == id && x.Category.ToString() == category.Key); //todo INt

                if (existing == null)
                    existing = new CategoryBudget() {UserId = id, Category = Enum.Parse<CategoryOfTransaction>(category.Key) };

                existing.PlanedBudget = decimal.Parse(category.Value);
                _planerContext.Update(existing);
            }

            _planerContext.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

    }
}
