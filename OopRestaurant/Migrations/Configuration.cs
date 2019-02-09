namespace OopRestaurant.Migrations
{
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

        }
    }
}
