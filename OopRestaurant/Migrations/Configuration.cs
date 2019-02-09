namespace OopRestaurant.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using OopRestaurant.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OopRestaurant.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OopRestaurant.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var category1 = new Category() { Name = "Levesek" }; //Id-t az Entity Framework ossza ki
            var category2 = new Category() { Name = "Hideg Eloetelek" };
            var category3 = new Category() { Name = "Meleg eloetelek" };

            context.Categories.AddOrUpdate(x => x.Name, category1, category2, category3);

            context.MenuItems.AddOrUpdate(x => x.Name, new MenuItem()
            {
                Name = "Tengeri hal Trio",
                Description = "Atlanti lazactar, pacolt lazacfile es tonhal lazackarikaval",
                Price = 7500,
                Category = category1
            });

            context.MenuItems.AddOrUpdate(x => x.Name, new MenuItem()
            {
                Name = "Borjueszencia",
                Description = "Zoldseges gyogytyuk galuska",
                Price = 4500,
                Category = category1
            });

            context.MenuItems.AddOrUpdate(x => x.Name, new MenuItem()
            {
                Name = "Szarvasgomba cappuccino",
                Price = 4500,
                Category = category1
            });

            context.MenuItems.AddOrUpdate(x => x.Name, new MenuItem()
            {
                Name = "Hirtelen sult fogasderek illatos erdei gombakkal",
                Price = 4500,
                Category = category3
            });

            context.MenuItems.AddOrUpdate(x => x.Name, new MenuItem()
            {
                Name = "Gundel Karoly gulyaslevese 1910",
                Price = 3500,
                Category = category1
            });

            context.MenuItems.AddOrUpdate(x => x.Name, new MenuItem()
            {
                Name = "Szaritott erlelt belszin carpaccio",
                Description = "Oreg trappista sajt, keseru levelek",
                Price = 5000,
                Category = category2
            });

            //Locations
            var loc1 = new Location { Name = "Nemdohanyzo terem", IsNonSmoking = true };
            var loc2 = new Location { Name = "Dohanyzo terem", IsNonSmoking = false };
            var loc3 = new Location { Name = "Terasz", IsNonSmoking = false };
            context.Locations.AddOrUpdate(x => x.Name, loc1, loc2, loc3);

            //Tables
            context.Tables.AddOrUpdate(x => x.Name,
                new Table { Name = "1. asztal", Location = loc1},
                new Table { Name = "2. asztal", Location = loc1 },
                new Table { Name = "3. asztal", Location = loc2 },
                new Table { Name = "4. asztal", Location = loc2},
                new Table { Name = "5. asztal", Location = loc3 },
                new Table { Name = "6. asztal", Location = loc3 }
                );

            var user = new ApplicationUser { UserName = "lehel@lehel.com", Email = "lehel@lehel.com" };

            var store = new UserStore<ApplicationUser>(context);
            var manager = new ApplicationUserManager(store);

            var userExists = manager.FindByEmail(user.Email);

            if (null == userExists)
            {
                var result = manager.Create(user, "aA!1234");

                if (!result.Succeeded)
                {
                    //var errorMessage = "";
                    //
                    ////Solutiion 1
                    //foreach (var error in result.Errors)
                    //{
                    //    if (string.IsNullOrEmpty(errorMessage))
                    //    {
                    //        errorMessage = error;
                    //    }
                    //    else
                    //    {
                    //        errorMessage = errorMessage + ", ";
                    //    }
                    //}

                    ////Solution 2 with less rows
                    //foreach (var error in result.Errors)
                    //{
                    //    errorMessage = errorMessage
                    //        + (string.IsNullOrEmpty(errorMessage) ? "" : ", ")
                    //        + error;
                    //}

                    //Solution 3 with 1 row
                    throw new Exception(string.Join(", ", result.Errors));

                }
            }

        }
    }
}
