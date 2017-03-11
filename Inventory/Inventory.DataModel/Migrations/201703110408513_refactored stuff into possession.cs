namespace Inventory.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoredstuffintopossession : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Stuffs", newName: "Possessions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Possessions", newName: "Stuffs");
        }
    }
}
