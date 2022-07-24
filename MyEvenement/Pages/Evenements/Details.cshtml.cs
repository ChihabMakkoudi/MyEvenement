using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEvenement.Data;
using MyEvenement.Models;

namespace MyEvenement.Pages.Evenements
{
    public class DetailsModel : PageModel
    {
        private readonly MyEvenement.Data.MyEvenementContext _context;

        public DetailsModel(MyEvenement.Data.MyEvenementContext context)
        {
            _context = context;
        }

        public Evenement Evenement { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Evenement = await _context.Evenement.FirstOrDefaultAsync(m => m.ID == id);

            if (Evenement == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
