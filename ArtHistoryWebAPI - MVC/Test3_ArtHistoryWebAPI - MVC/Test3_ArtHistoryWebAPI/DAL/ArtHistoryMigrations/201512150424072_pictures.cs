namespace Test3_ArtHistoryWebAPI.DAL.ArtHistoryMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pictures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Painting", "imageContent", c => c.Binary());
            AddColumn("dbo.Painting", "imageMimeType", c => c.String(maxLength: 256));
            AddColumn("dbo.Painting", "imageFileName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Painting", "imageFileName");
            DropColumn("dbo.Painting", "imageMimeType");
            DropColumn("dbo.Painting", "imageContent");
        }
    }
}
