using MyEvenement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyEvenement.Authorization;

// dotnet aspnet-codegenerator razorpage -m Contact -dc MyEvenementContext -udl -outDir Pages\Contacts --referenceScriptLibraries

namespace MyEvenement.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new MyEvenementContext(
                serviceProvider.GetRequiredService<DbContextOptions<MyEvenementContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
                await EnsureRole(serviceProvider, adminID, Constants.AdministratorsRole);

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
                await EnsureRole(serviceProvider, managerID, Constants.ManagersRole);

                SeedDB(context, adminID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<AppUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void SeedDB(MyEvenementContext context, string adminID)
        {
            if (context.Inscription.Any())
            {
                return;   // DB has been seeded
            }

            context.Inscription.AddRange(
             new Inscription
             {
                 Nom = "nom inscript",
                 Prenom = "per",
                 Email = "mail@mail.com",
                 Nationalite = "nationalite",
                 Telephone = "066666666666",
                 Adress = "adress",
                 EvenementID = 6,
                 OwnerID = adminID,
                 Status = InscriptionStatus.Submitted
             },
             new Inscription
             {
                 Nom = "De inscript",
                 Prenom = "de per",
                 Email = "demail@mail.com",
                 Nationalite = "denationalite",
                 Telephone = "066666666666",
                 Adress = "deadress",
                 EvenementID = 7,
                 OwnerID = adminID,
                 Status = InscriptionStatus.Approved
             },
             new Inscription
             {
                 Nom = "te inscript",
                 Prenom = "te per",
                 Email = "temail@mail.com",
                 Nationalite = "tenationalite",
                 Telephone = "066666666666",
                 Adress = "teadress",
                 EvenementID = 8,
                 OwnerID = adminID,
                 Status = InscriptionStatus.Rejected
             }
             );
            context.SaveChanges();
        }

    }
}