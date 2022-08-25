using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyEvenement.Models;

namespace MyEvenement.Data
{
    public class MyEvenementContext : IdentityDbContext<AppUser>
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
        public DbSet<Statut> Statut { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserIdentityConfiguration());
        }

        private class ApplicationUserIdentityConfiguration : IEntityTypeConfiguration<AppUser>
        {
            public void Configure(EntityTypeBuilder<AppUser> builder)
            {
                builder.Property(u => u.Prenom).HasMaxLength(255);
                builder.Property(u => u.Nom).HasMaxLength(255);
            }
        }
    }
}
