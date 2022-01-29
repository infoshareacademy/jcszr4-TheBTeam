using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;
using TheBTeam.BLL.Services;
using TheBTeam.Web.Models;

namespace TheBTeam.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PlannerContext _plannerContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<HomeController> logger, PlannerContext plannerContext, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _plannerContext = plannerContext;
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            var userClaims = _applicationDbContext.UserClaims.Count();
            if (userClaims > 0)
            {
                var loadRoleClaims = new LoadRoleClaims();
                ViewBag.Role = loadRoleClaims.GetLastRoleClaim(_applicationDbContext);
            }
            LoadDataFromFile.LoadUsersToDatbase(_plannerContext);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public IActionResult Dashboard(string role)//TODO to musi byc aby dashboard sie pojawil
        {
            ViewBag.Role = role;
            return View();
        }
    }
}
