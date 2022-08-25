using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEvenement.Data;
using MyEvenement.Models;

namespace MyEvenement.Pages.Statuts
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
            return Page();
        }

        [BindProperty]
        public Statut Statut { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Statut.Add(Statut);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
