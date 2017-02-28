namespace Inventory.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingStuff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stuffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Acquired = c.DateTime(nullable: false),
                        Category = c.Int(nullable: false),
                        SubCategory = c.String(),
                        InUse = c.Boolean(nullable: false),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Owners", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stuffs", "Owner_Id", "dbo.Owners");
            DropIndex("dbo.Stuffs", new[] { "Owner_Id" });
            DropTable("dbo.Stuffs");
        }
    }
}
