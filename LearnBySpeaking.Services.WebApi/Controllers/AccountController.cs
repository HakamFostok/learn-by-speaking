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
            IdentityUser user = new IdentityUser
            {
                UserName = loginViewModel.UserName,
                Email = loginViewModel + "@learnbyspeaking.com"
            };

            var result1 = await _userManager.CreateAsync(user, loginViewModel.Password);
            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);
            if (result.Succeeded)
                return RedirectToAction("CreateTest", "Test");

            return View(loginViewModel);
        }
    }
}
