using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyEvenement.Data;
using System.Linq;

namespace MyEvenement.Controllers
{
    public class FileController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public FileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public FileContentResult Photo(AppUser user)
        {
        
            // find the user. I am skipping validations and other checks.
            return new FileContentResult(user.ProfilePicture, "image/jpeg");
        }
    }
}
