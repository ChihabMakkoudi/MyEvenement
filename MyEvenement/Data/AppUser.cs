using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyEvenement.Data
{
    // Add profile data for application users by adding properties to the AppUser class
    public class AppUser : IdentityUser
    {
        public string Nom { get; set; }

        public string Prenom { get; set; }

        public byte[] ProfilePicture { get; set; }

    }
}
