using System.ComponentModel.DataAnnotations;

namespace   MyEvenement.Models
{
    public class Inscription
    {
        public int InscriptionID { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Nationalite { get; set; }
        public string Adress { get; set; }
        public string Telephone { get; set; }

        public int DetailID { get; set; }
        public Detail Detail { get; set; }

        public int EvenementID { get; set; }
        public Evenement Evenement { get; set; }

        public string OwnerID { get; set; }
    }
}