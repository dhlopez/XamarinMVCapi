namespace Test3_ArtHistoryWebAPI.DAL.ArtHistoryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artist",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        WholeName = c.String(nullable: false, maxLength: 255),
                        YearOfBirth = c.String(nullable: false, maxLength: 4),
                        Country = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true, name: "IX_Unique_Artist");
            
            CreateTable(
                "dbo.Movement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Period = c.String(nullable: false, maxLength: 12),
                        Characteristics = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Painting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 70),
                        Dated = c.String(nullable: false, maxLength: 4),
                        MovementID = c.Int(nullable: false),
                        ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Artist", t => t.ArtistID)
                .ForeignKey("dbo.Movement", t => t.MovementID)
                .Index(t => t.MovementID)
                .Index(t => t.ArtistID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Painting", "MovementID", "dbo.Movement");
            DropForeignKey("dbo.Painting", "ArtistID", "dbo.Artist");
            DropIndex("dbo.Painting", new[] { "ArtistID" });
            DropIndex("dbo.Painting", new[] { "MovementID" });
            DropIndex("dbo.Artist", "IX_Unique_Artist");
            DropTable("dbo.Painting");
            DropTable("dbo.Movement");
            DropTable("dbo.Artist");
        }
    }
}
