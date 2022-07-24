using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEvenement.Data;
using MyEvenement.Models;

namespace MyEvenement.Pages.Inscriptions
{
    public class IndexModel : PageModel
    {
        private readonly MyEvenement.Data.MyEvenementContext _context;

        public IndexModel(MyEvenement.Data.MyEvenementContext context)
        {
            _context = context;
        }

        public IList<Inscription> Inscription { get;set; }

        public async Task OnGetAsync()
        {
            Inscription = await _context.Inscription
                .Include(i => i.Evenement).ToListAsync();
        }
    }
}
