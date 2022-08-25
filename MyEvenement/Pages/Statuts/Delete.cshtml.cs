using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEvenement.Data;
using MyEvenement.Models;

namespace MyEvenement.Pages.Statuts
{
    public class DeleteModel : PageModel
    {
        private readonly MyEvenement.Data.MyEvenementContext _context;

        public DeleteModel(MyEvenement.Data.MyEvenementContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Statut = await _context.Statut.FindAsync(id);

            if (Statut != null)
            {
                _context.Statut.Remove(Statut);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
