using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MyEvenement.Controllers
{
    [Controller]
    [Route("Home")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/Register");
            return View();
        }
        [HttpGet]
        [Route("ChangeLanguage")]
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            Console.WriteLine("sometging");
            //return Redirect("/Register");
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
