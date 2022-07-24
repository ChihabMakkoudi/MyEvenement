using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyEvenement.Models;

namespace MyEvenement.Data
{
    public class MyEvenementContext : DbContext
    {
        public MyEvenementContext (DbContextOptions<MyEvenementContext> options)
            : base(options)
        {
        }

        public DbSet<MyEvenement.Models.Evenement> Evenement { get; set; }

        public DbSet<MyEvenement.Models.Inscription> Inscription { get; set; }
    }
}
