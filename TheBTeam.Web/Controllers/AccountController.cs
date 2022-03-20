using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TheBTeam.BLL;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;
using TheBTeam.BLL.Models;
using TheBTeam.BLL.Services;
using TheBTeam.Web.Services;

namespace TheBTeam.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        private readonly PlannerContext _plannerContext;
        private readonly UserService _userService;
        private readonly CategoryLogService _categoryLogService;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger, PlannerContext plannerContext, UserService userService, CategoryLogService categoryLogService)
        {
            _accountService = accountService;
            _logger = logger;
            _plannerContext = plannerContext;
            _userService = userService;
            _categoryLogService = categoryLogService;
        }

        [Authorize]
        public async Task<IActionResult> Index(string email)
        {
            ViewBag.Email = email;
            var usersList = await _userService.GetAllUsers();
            return View(usersList);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Register()
        {
            RegisterDto model = new RegisterDto();
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            if (ModelState.IsValid)
            {
                //TODO: add scope
                using var log = _logger.BeginScope("UserEmailCheckInRegistration");
                _logger.LogDebug("Checking if user exists {userEmail}", request.Email);
                var userCheck = await _plannerContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

                if (userCheck == null)
                {
                    var user = new User
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Balance = 0m,
                        Age = 99,
                        Gender = Gender.Male,
                        Email = request.Email,
                        Role = _plannerContext.Roles.Find(2)
                    };

                    var password = Base64EncodeDecode.Base64Encode(request.Password);
                    user.PasswordHash = password;
                    var result = _plannerContext.Add(user);
                    if (result != null)
                    {
                        _logger.LogInformation($"User {request.Email} created successfully");
                        _plannerContext.SaveChanges();
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        _logger.LogWarning($"Adding new user {request.Email} not successfully");
                        ModelState.AddModelError("message", "Error with adding new user");
                        return View(request);
                    }
                }
                else
                {
                    _logger.LogInformation($"User with this email {request.Email} already exists");
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }
            return View(request);

        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var authResult = await ValidateLogin(userName, password);
            // Normally Identity handles sign in, but you can do it directly
            if (authResult.Success == true)
            {
                var claims = new List<Claim>
                {
                    new Claim("user", authResult.UserName),
                    new Claim("role", authResult.RoleName)
                };

                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));
                

                if (authResult.RoleName == "Admin")
                {
                    _logger.LogInformation($"User {userName} login successfully");
                    
                    var findUserId = _plannerContext.Users.Where(u => u.Email == authResult.UserName).Select(u => u.Id).FirstOrDefault();

                    _categoryLogService.LogInOutcome(findUserId, DateTime.Now);

                    return RedirectToAction("Index", "Account", new { email = userName, role = authResult.RoleName });
                }
                else
                {
                    _logger.LogInformation($"User {userName} login successfully");

                    var findUserId = _plannerContext.Users.Where(u => u.Email == authResult.UserName).Select(u => u.Id).FirstOrDefault();

                    _categoryLogService.LogInOutcome(findUserId, DateTime.Now);

                    return RedirectToAction("Index", "Account", new { email = userName, role = authResult.RoleName });
                }
                
            }
            else
            {
                _logger.LogWarning($"invalid login attempt for user {userName}");
            }

            return View("UserIsNotRegistered","Account");
        }

        private async Task<LoginResult> ValidateLogin(string userName, string password)
        {
            var authResult = await _accountService.ValidateUser(userName, password);
            return authResult;
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
