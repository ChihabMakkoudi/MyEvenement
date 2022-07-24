using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyEvenement.Data;
using MyEvenement.Models;

namespace MyEvenement.Pages.Inscriptions
{
    public class EditModel : PageModel
    {
        private readonly MyEvenement.Data.MyEvenementContext _context;

        public EditModel(MyEvenement.Data.MyEvenementContext context)
        {
            _context = context;
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
            Inscription = inscription;
            ViewData["EvenementID"] = new SelectList(_context.Evenement, "ID", "Nom");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Inscription).State = EntityState.Modified;

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
