namespace Inventory.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedselfreference : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stuffs", "PartOf_Id", c => c.Int());
            CreateIndex("dbo.Stuffs", "PartOf_Id");
            AddForeignKey("dbo.Stuffs", "PartOf_Id", "dbo.Stuffs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stuffs", "PartOf_Id", "dbo.Stuffs");
            DropIndex("dbo.Stuffs", new[] { "PartOf_Id" });
            DropColumn("dbo.Stuffs", "PartOf_Id");
        }
    }
}
