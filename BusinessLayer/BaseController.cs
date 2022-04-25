
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace VeriPark.DigitalBadge.Business
{
    public class BaseController : Controller
    {
        protected readonly UserManager<User> userManager;
        protected readonly SignInManager<User> signInManager;
        public const int _pageSize = 1;
        public BaseController()
        {

        }

        public  int GetCurrentId()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Convert.ToInt32(id);
        }
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


    }
}
