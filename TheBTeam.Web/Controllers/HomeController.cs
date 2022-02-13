using Microsoft.AspNetCore.Authorization;
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
        private readonly UserService _userService;


        public HomeController(ILogger<HomeController> logger, PlannerContext plannerContext, UserService userService)
        {
            _logger = logger;
            _plannerContext = plannerContext;
            _userService = userService;
        }

        //[Authorize]//[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string email)
        {
            ViewBag.Email = email;
            var usersList = await _userService.GetAllUsers();
            return View(usersList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnly()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
