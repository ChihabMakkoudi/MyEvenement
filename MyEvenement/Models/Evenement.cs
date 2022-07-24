using System;
using System.Collections.Generic;

namespace MyEvenement.Models
{
    public class Evenement
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Inscription> Inscriptions { get; set; }
    }
}