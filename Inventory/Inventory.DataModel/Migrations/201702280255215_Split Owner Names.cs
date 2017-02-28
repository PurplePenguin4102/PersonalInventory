namespace Inventory.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SplitOwnerNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Owners", "FirstName", c => c.String());
            AddColumn("dbo.Owners", "LastName", c => c.String());
            DropColumn("dbo.Owners", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Owners", "Name", c => c.String());
            DropColumn("dbo.Owners", "LastName");
            DropColumn("dbo.Owners", "FirstName");
        }
    }
}
