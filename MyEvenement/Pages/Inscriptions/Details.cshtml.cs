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
    public class DetailsModel : DI_BasePageModel
    {
        //private readonly MyEvenement.Data.MyEvenementContext _context;

        public DetailsModel(
        MyEvenementContext context,
        IAuthorizationService authorizationService,
        UserManager<AppUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        public Inscription Inscription { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Inscription = await _context.Inscription
                .AsNoTracking()
                .Include(c => c.Evenement)
                .FirstOrDefaultAsync(m => m.InscriptionID == id);

            if (Inscription == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Constants.ManagersRole) ||
                           User.IsInRole(Constants.AdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != Inscription.OwnerID
                && Inscription.Status != InscriptionStatus.Approved)
            {
                return Forbid();
            }


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, InscriptionStatus status)
        {
            var contact = await base._context.Inscription.FirstOrDefaultAsync(
                                                      m => m.InscriptionID == id);

            if (contact == null)
            {
                return NotFound();
            }

            var contactOperation = (status == InscriptionStatus.Approved)
                                                       ? InscriptionOperations.Approve
                                                       : InscriptionOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, contact,
                                        contactOperation);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            contact.Status = status;
            base._context.Inscription.Update(contact);
            await base._context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
