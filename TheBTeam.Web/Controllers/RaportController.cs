using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBTeam.Web.Services;

namespace TheBTeam.Web.Controllers
{
    public class RaportController : Controller
    {
        private CategoryLogService _categoryLogService;
        private IConfiguration Configuration { get; }

        public RaportController(IConfiguration configuration)
        {
            Configuration = configuration;
            _categoryLogService = new CategoryLogService(configuration);
        }
        public ActionResult Index()
        {
            var report=_categoryLogService.GetReport();
            return View(report);
        }

        // GET: RaportController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RaportController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RaportController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RaportController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RaportController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RaportController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RaportController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
