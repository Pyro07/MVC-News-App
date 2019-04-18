namespace MVCHaberV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Habers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(),
                        Detay = c.String(),
                        HaberResim = c.String(),
                        KategoriId = c.Int(nullable: false),
                        KategorininAdi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Yorums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Icerik = c.String(),
                        Tarih = c.DateTime(nullable: false),
                        Haber_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Habers", t => t.Haber_Id)
                .Index(t => t.Haber_Id);
            
            CreateTable(
                "dbo.Kategoris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorums", "Haber_Id", "dbo.Habers");
            DropIndex("dbo.Yorums", new[] { "Haber_Id" });
            DropTable("dbo.Kategoris");
            DropTable("dbo.Yorums");
            DropTable("dbo.Habers");
        }
    }
}
