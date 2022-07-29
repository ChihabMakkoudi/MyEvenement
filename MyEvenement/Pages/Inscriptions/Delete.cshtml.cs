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
    public class DeleteModel : DI_BasePageModel
    {
        //private readonly MyEvenement.Data.MyEvenementContext _context;

        public DeleteModel(
        MyEvenementContext context,
        IAuthorizationService authorizationService,
        UserManager<AppUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }


        [BindProperty]
        public Inscription Inscription { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Inscription = await _context.Inscription
                .Include(i => i.Evenement).FirstOrDefaultAsync(m => m.InscriptionID == id);

            if (Inscription == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Inscription,
                                                     InscriptionOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }




            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
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


            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                 User, Inscription,
                                                 InscriptionOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }



            _context.Inscription.Remove(Inscription);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
