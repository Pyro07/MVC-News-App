namespace MVCHaberV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HaberDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Yorums", "Haber_Id", "dbo.Habers");
            DropIndex("dbo.Yorums", new[] { "Haber_Id" });
            AddColumn("dbo.Yorums", "Haber_Id1", c => c.Int());
            AlterColumn("dbo.Yorums", "Haber_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Yorums", "Haber_Id1");
            AddForeignKey("dbo.Yorums", "Haber_Id1", "dbo.Habers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorums", "Haber_Id1", "dbo.Habers");
            DropIndex("dbo.Yorums", new[] { "Haber_Id1" });
            AlterColumn("dbo.Yorums", "Haber_Id", c => c.Int());
            DropColumn("dbo.Yorums", "Haber_Id1");
            CreateIndex("dbo.Yorums", "Haber_Id");
            AddForeignKey("dbo.Yorums", "Haber_Id", "dbo.Habers", "Id");
        }
    }
}
