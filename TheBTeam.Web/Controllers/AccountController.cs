using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TheBTeam.BLL.Enums;
using TheBTeam.BLL.Models;
using TheBTeam.BLL.Services;

namespace TheBTeam.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._logger = logger;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            UserRegistrationDto model = new UserRegistrationDto();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            if (ModelState.IsValid)
            {
                //TODO: add scope
                using var log = _logger.BeginScope("UserEmailCheckInRegistration");
                _logger.LogDebug("Checking if user exists {userEmail}", request.Email);
                var userCheck = await _userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };
                    _logger.LogDebug("Creating new user");
                    var result = await _userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogDebug("User created successfully");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
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
        [AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginDto model = new UserLoginDto();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {

            if (ModelState.IsValid)
            {
                var loadRoleClaims = new LoadRoleClaims();
                var info  = await loadRoleClaims.SetRoleClaim(model,_userManager,_signInManager);

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {

                    _logger.LogInformation($"User {model.Email} log at {DateTime.Now}");

                    var userRoles = await _userManager.GetClaimsAsync(user);
                    var userRole = userRoles.Select(u => u.Value).Last();//last logged

                    ViewBag.Role = model.Email;
                    return RedirectToAction("Dashboard", "Home", new { test = info });
                }
                else if (result.IsLockedOut)
                {
                    _logger.LogError("User {email} account locked!!", model.Email);
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    _logger.LogWarning($"invalid login attempt for user {model.Email}");
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult AccountUser()
        {
            return View();
        }
        public ActionResult EmptAccountUseryList()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation($"User logout at {DateTime.Now}");
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }
        public ActionResult Delete()
        {
            return View();
        }
    }
}
