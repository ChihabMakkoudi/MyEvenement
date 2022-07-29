using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyEvenement.Authorization;
using MyEvenement.Data;
using MyEvenement.Models;

namespace MyEvenement.Pages.Inscriptions
{
    public class EditModel : DI_BasePageModel
    {
        //private readonly MyEvenement.Data.MyEvenementContext _context;

        public EditModel(
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
            if (id == null || _context.Inscription == null)
            {
                return NotFound();
            }

            var inscription =  await _context.Inscription.FirstOrDefaultAsync(m => m.InscriptionID == id);
            if (inscription == null)
            {
                return NotFound();
            }
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                  User, Inscription,
                                                  InscriptionOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Inscription = inscription;
            ViewData["EvenementID"] = new SelectList(_context.Evenement, "ID", "Nom");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Fetch Contact from DB to get OwnerID.
            var inscription = await _context
                .Inscription.AsNoTracking()
                .FirstOrDefaultAsync(m => m.InscriptionID == id);

            if (inscription == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, inscription,
                                                     InscriptionOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Inscription.OwnerID = inscription.OwnerID;


            _context.Attach(Inscription).State = EntityState.Modified;


            if (Inscription.Status == InscriptionStatus.Approved)
            {
                // If the contact is updated after approval, 
                // and the user cannot approve,
                // set the status back to submitted so the update can be
                // checked and approved.
                var canApprove = await AuthorizationService.AuthorizeAsync(User,
                                        Inscription,
                                        InscriptionOperations.Approve);

                if (!canApprove.Succeeded)
                {
                    Inscription.Status = InscriptionStatus.Submitted;
                }
            }



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscriptionExists(Inscription.InscriptionID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InscriptionExists(int id)
        {
            return _context.Inscription.Any(e => e.InscriptionID == id);
        }
    }
}
