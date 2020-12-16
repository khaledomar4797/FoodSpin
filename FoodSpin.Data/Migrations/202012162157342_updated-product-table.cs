namespace FoodSpin.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedproducttable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "OwnerId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "OwnerId");
        }
    }
}
