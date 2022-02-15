using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

namespace TheBTeam.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        private readonly PlannerContext _plannerContext;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger, PlannerContext plannerContext)
        {
            _accountService = accountService;
            _logger = logger;
            _plannerContext = plannerContext;
        }


        [Authorize(Roles = "Admin")]
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
                        FirstName = "User",
                        LastName = "User",
                        Age = 99,
                        Gender = Gender.Male,
                        Email = request.Email,
                        Role = _plannerContext.Roles.Find(2)
                    };

                    IPasswordHasher<User> hasher = new PasswordHasher<User>();
                    var password = hasher.HashPassword(user, request.Email);
                    user.PasswordHash = password;
                    var result = _plannerContext.Add(user);
                    if (result != null)
                    {
                        _logger.LogDebug("User created successfully");
                        _plannerContext.SaveChanges();
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("message", "Error with adding new user");
                        return View(request);
                    }
                }
                else
                {
                    _logger.LogDebug("User with this email already exists");
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

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                    //return RedirectToAction("Index", "User", new { email = userName , role = authResult.RoleName });
                }
                else
                {
                    return Redirect("/");
                }
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
