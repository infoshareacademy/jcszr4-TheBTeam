using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        [HttpGet]//[HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            RegisterDto model = new RegisterDto();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            if (ModelState.IsValid)
            {
                //TODO: add scope
                using var log = _logger.BeginScope("UserEmailCheckInRegistration");
                _logger.LogDebug("Checking if user exists {userEmail}", request.Email);
                //var userCheck = await _plannerContext.FindAsync(e=>e. request.Email);
                
                var userCheck = await _plannerContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

                if (userCheck == null)
                {
                    var user = new User
                    {
                        FirstName = "",
                        LastName = "",
                        Balance = 0,
                        Currency = BLL.Currency.PLN,
                        Age = 18,
                        Gender = BLL.Gender.Genderless,
                        Email = request.Email,
                        CreatedAt = DateTime.Now,
                        //TODO nadanie roli i hasla
                        //Role = ""
                        //Password = request.Password,
                        //ConfirmPassword = request.ConfirmPassword
                    };
                    _logger.LogDebug("Creating new user");

                    _plannerContext.Users.Add(user);
                    await _plannerContext.SaveChangesAsync();
                    //TODO validation here add
                    _logger.LogDebug("User created successfully");
                    return RedirectToAction("/");

                    //TODO validate
                    //var result = await _userManager.CreateAsync(user, request.Password);
                    //if (result.Succeeded)
                    //{
                    //    _logger.LogDebug("User created successfully");
                    //    return RedirectToAction("Login");
                    //}
                    //else
                    //{
                    //    if (result.Errors.Count() > 0)
                    //    {
                    //        foreach (var error in result.Errors)
                    //        {
                    //            ModelState.AddModelError("message", error.Description);
                    //        }
                    //    }
                    //    return View(request);
                    //}
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
                    //return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home", new { email = userName });
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
