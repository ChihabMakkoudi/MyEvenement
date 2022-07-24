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

        public DbSet<Evenement> Evenement { get; set; }
        public DbSet<Inscription> Inscription { get; set; }
        public DbSet<Detail> Detai { get; set; }
        public DbSet<DetailNational> DetailNational { get; set; }
        public DbSet<DetailInternational> DetailInternational { get; set; }
    }
}
