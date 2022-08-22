using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyEvenement.Data;
using MyEvenement.Models;
using MyEvenement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace MyEvenement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly MyEvenementContext _context;

        public IndexModel(ILogger<IndexModel> logger, MyEvenementContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Evenement Evenement { get; set; }

        [BindProperty]
        public Inscription Inscription { get; set; }

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

            /*MailSender mailSender = new("testsender420@yahoo.com",      // to
                                        "testsender420@outlook.com",    // from
                                        "final test",     // subject
                                        "this mail is sent automaticaly by using asp.net and smtp"      //body
                                        );
            mailSender.send();*/
            // ViewData["EvenementID"] = new SelectList(_context.Evenement, "ID", "Nom");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var userid = _userManager.GetUserAsync(User).Result.Id;
            Console.WriteLine(userid);
            var inscription = await _context.Inscription.FirstOrDefaultAsync(m => m.OwnerID == userid);
            if (inscription == null)
            {
                return NotFound();
            }
            Console.WriteLine(userid);
            Console.WriteLine(inscription.InscriptionID);
            return RedirectToPage("/Inscriptions/Details", new { id=inscription.InscriptionID});
        }
    }
}
