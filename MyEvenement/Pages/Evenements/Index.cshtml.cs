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
    public class IndexModel : PageModel
    {
        private readonly MyEvenement.Data.MyEvenementContext _context;

        public IndexModel(MyEvenement.Data.MyEvenementContext context)
        {
            _context = context;
        }

        public IList<Evenement> Evenement { get;set; }

        public async Task OnGetAsync()
        {
            Evenement = await _context.Evenement.ToListAsync();
        }
    }
}
