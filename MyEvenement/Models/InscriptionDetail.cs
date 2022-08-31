using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyEvenement.Data;
using System;
using System.Threading.Tasks;

namespace MyEvenement.Models
{
    public class InscriptionDetail
    {
        public MyEvenementContext _context { get; set; }

        public Inscription Inscription { get; set; }

        public Evenement Evenement { get; set; }

        public IFormFile PieceJointe { get; set; }

        public DetailInternational DetailInternational { get; set; }

        public DetailNational DetailNational { get; set; }

        public InscriptionDetail()
        {
        }

        public InscriptionDetail(MyEvenementContext context)
        {
            _context = context;
        }
        

        public async Task SetEvent(int eventId)
        {
            //set evenement
            var evenement = await _context.Evenement.FirstOrDefaultAsync(m => m.ID == eventId);
            if (evenement == null)
            {
                Console.WriteLine("notfound");
            }
            Evenement = evenement;
        }

        public async Task RegisterInscription(string userId)
        {
            // register inscription

            //var evenement = await _context.Evenement.FirstOrDefaultAsync(m => m.ID == 6);
            if (Evenement.TypeDetail.Equals("DetailInternational"))
            {
                Inscription.Detail = DetailInternational;
            }
            else if (Evenement.TypeDetail.Equals("DetailNational"))
            {
                Inscription.Detail = DetailNational;
            }
            Inscription.EvenementID = Evenement.ID;
            Inscription.Statut = await _context.Statut.FirstOrDefaultAsync(m => m.StatutName == "En attente de validation");
            Inscription.OwnerID = userId;
            _context.Inscription.Add(Inscription);
            await _context.SaveChangesAsync();
        }
    }
}
