using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEvenement.Data;
using MyEvenement.Models;

namespace MyEvenement.Pages.Inscriptions
{
    public class DetailsModel : PageModel
    {
        private readonly MyEvenement.Data.MyEvenementContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DetailsModel(MyEvenement.Data.MyEvenementContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Inscription Inscription { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                var userid = _userManager.GetUserAsync(User).Result.Id;
                Console.WriteLine(userid);
                Inscription = await _context.Inscription.FirstOrDefaultAsync(m => m.OwnerID == userid);
                if (Inscription == null)
                {
                    return NotFound();
                }
                return Page();
            }

            Inscription = await _context.Inscription
                .AsNoTracking()
                .Include(c => c.Evenement)
                .FirstOrDefaultAsync(m => m.InscriptionID == id);

            if (Inscription == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
