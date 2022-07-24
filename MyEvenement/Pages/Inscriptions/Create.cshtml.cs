using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEvenement.Data;
using MyEvenement.Models;

namespace MyEvenement.Pages.Inscriptions
{
    public class CreateModel : PageModel
    {
        private readonly MyEvenement.Data.MyEvenementContext _context;

        public CreateModel(MyEvenement.Data.MyEvenementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EvenementID"] = new SelectList(_context.Evenement, "ID", "Nom");
            return Page();
        }

        [BindProperty]
        public Inscription Inscription { get; set; }

        [BindProperty]
        public DetailInternational DetailInternational { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Inscription.Detail = DetailInternational;
            _context.Inscription.Add(Inscription);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
