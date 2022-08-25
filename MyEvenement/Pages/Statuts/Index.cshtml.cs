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
    public class IndexModel : PageModel
    {
        private readonly MyEvenement.Data.MyEvenementContext _context;

        public IndexModel(MyEvenement.Data.MyEvenementContext context)
        {
            _context = context;
        }

        public IList<Statut> Statut { get;set; }

        public async Task OnGetAsync()
        {
            Statut = await _context.Statut.ToListAsync();
        }
    }
}
