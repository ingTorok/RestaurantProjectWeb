namespace OopRestaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMenuitemPricecolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItems", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuItems", "Price");
        }
    }
}
