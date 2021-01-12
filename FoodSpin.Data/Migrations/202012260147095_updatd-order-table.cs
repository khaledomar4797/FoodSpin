namespace FoodSpin.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class updatdordertable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Order", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Order", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Order", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Order", "State", c => c.String(nullable: false));
            AlterColumn("dbo.Order", "PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.Order", "Phone", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Order", "Phone", c => c.String());
            AlterColumn("dbo.Order", "PostalCode", c => c.String());
            AlterColumn("dbo.Order", "State", c => c.String());
            AlterColumn("dbo.Order", "City", c => c.String());
            AlterColumn("dbo.Order", "Address", c => c.String());
            AlterColumn("dbo.Order", "LastName", c => c.String());
            AlterColumn("dbo.Order", "FirstName", c => c.String());
        }
    }
}
