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
using MyEvenement.Utils;

namespace MyEvenement.Pages.Inscriptions
{
    public class CreateModel : PageModel
    {
        private readonly MyEvenementContext _context;

        public CreateModel(MyEvenementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            int id = ConfigData.GetConfigData_Json().Current_Event_Id;
            if (id == null || _context.Evenement == null)
            {
                return NotFound();
            }

            var evenement = await _context.Evenement.FirstOrDefaultAsync(m => m.ID == id);
            if (evenement == null)
            {
                return NotFound();
            }
            Evenement = evenement;

            // ViewData["EvenementID"] = new SelectList(_context.Evenement, "ID", "Nom");
            return Page();
        }

        [BindProperty]
        public Inscription Inscription { get; set; }
        
        [BindProperty]
        public Evenement Evenement { get; set; }

        [BindProperty]
        public DetailInternational DetailInternational { get; set; }

        [BindProperty]
        public DetailNational DetailNational { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var evenement = await _context.Evenement.FirstOrDefaultAsync(m => m.ID == Evenement.ID);
            if (evenement.TypeDetail.Equals("DetailInternational"))
            {
                Inscription.Detail = DetailInternational;
            }
            else if (evenement.TypeDetail.Equals("DetailNational"))
            {
                Inscription.Detail = DetailNational;
            }
            Inscription.EvenementID = Evenement.ID;
            _context.Inscription.Add(Inscription);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
