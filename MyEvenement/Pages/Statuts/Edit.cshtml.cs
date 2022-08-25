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

namespace MyEvenement.Pages.Statuts
{
    public class EditModel : PageModel
    {
        private readonly MyEvenement.Data.MyEvenementContext _context;

        public EditModel(MyEvenement.Data.MyEvenementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Statut Statut { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Statut = await _context.Statut.FirstOrDefaultAsync(m => m.StatutID == id);

            if (Statut == null)
            {
                return NotFound();
            }
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

            _context.Attach(Statut).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatutExists(Statut.StatutID))
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

        private bool StatutExists(int id)
        {
            return _context.Statut.Any(e => e.StatutID == id);
        }
    }
}
