using BusinessProcessManagementSampleProject.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeriPark.DigitalBadge.Business;

namespace BusinessProcessManagementSampleProject.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {

        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager):base()
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Email = model.Mail,
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login","Account");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return LocalRedirect("/Project/CreateProject");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel u)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(u.UserName, u.Password, u.RememberMe, lockoutOnFailure:false);
               
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Project");
                }
                else
                {
                    ModelState.AddModelError("", "Hatalı Giriş");
                    return View(u);
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
