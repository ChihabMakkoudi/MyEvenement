using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEvenement.Authorization;
using MyEvenement.Data;
using MyEvenement.Models;

namespace MyEvenement.Pages.Inscriptions
{
    public class IndexModel : DI_BasePageModel
    {
        //private readonly MyEvenement.Data.MyEvenementContext _context;

        public IndexModel(
        MyEvenementContext context,
        IAuthorizationService authorizationService,
        UserManager<AppUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        public IList<Inscription> Inscription { get;set; }

        public async Task OnGetAsync()
        {
            var inscriptions = from c in base._context.Inscription
                           select c;

            var isAuthorized = User.IsInRole(Constants.ManagersRole) ||
                           User.IsInRole(Constants.AdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only approved contacts are shown UNLESS you're authorized to see them
            // or you are the owner.
            if (!isAuthorized)
            {
                inscriptions = inscriptions.Where(c => c.Status == InscriptionStatus.Approved
                                            || c.OwnerID == currentUserId);
            }
            Inscription = await inscriptions.ToListAsync();

            // Inscription = await _context.Inscription
            //     .Include(i => i.Evenement).ToListAsync();
        }
    }
}
