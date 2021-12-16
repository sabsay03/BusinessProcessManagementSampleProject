using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(User u)
        {
            {
                Context c = new Context();
                var dataValue = c.Users.FirstOrDefault(x => x.UserName == u.UserName && x.Password == u.Password);
                if (dataValue != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, u.UserName)
                        };
                    var useridentity = new ClaimsIdentity(claims, "a");
                    ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Group");


                }
                else
                {
                    return View();
                }

            }
        }
    }
}
