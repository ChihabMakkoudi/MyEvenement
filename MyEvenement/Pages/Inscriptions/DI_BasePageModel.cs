using MyEvenement.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyEvenement.Pages.Inscriptions
{
    public class DI_BasePageModel : PageModel
    {
        protected MyEvenementContext _context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<AppUser> UserManager { get; }

        public DI_BasePageModel(
            MyEvenementContext context,
            IAuthorizationService authorizationService,
            UserManager<AppUser> userManager) : base()
        {
            _context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        }
    }
}