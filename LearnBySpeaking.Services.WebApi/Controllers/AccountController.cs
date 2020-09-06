using LearnBySpeaking.Application.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace LearnBySpeaking.Services.WebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            // this is not BEST practice at all
            // this is just for demo purposes
            await CreateDefaultUserIfNotExists();
            
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, true, false);
            if (result.Succeeded)
                return RedirectToAction("CreateTest", "Test");

            ModelState.AddModelError("", "UserName or password are not correct");
            return View(loginViewModel);
        }

        private async Task CreateDefaultUserIfNotExists()
        {
            IdentityUser user = await _userManager.FindByEmailAsync("admin@learn.com");
            if (user != null)
                return;

          
            user = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin@learn.com",
                Email = "admin@learn.com"
            };
            
            await _userManager.CreateAsync(user, "123");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
