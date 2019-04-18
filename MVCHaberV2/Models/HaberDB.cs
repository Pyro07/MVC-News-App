namespace MVCHaberV2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    public class HaberDB : DbContext
    {
        // Your context has been configured to use a 'HaberDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MVCHaberV2.Models.HaberDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'HaberDB' 
        // connection string in the application configuration file.
        public HaberDB()
            : base("name=HaberDB")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Kategori> Kategoriler { get; set; }
        public virtual DbSet<Yorum> Yorumlar { get; set; }
        public virtual DbSet<Haber> Haberler { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
    public class Kategori
    {
        public int Id { get; set; }
        public string KategoriAdi { get; set; }
    }

    public class Yorum
    {
        public int Id { get; set; }
        public string Icerik { get; set; }
        public DateTime Tarih { get; set; }
        public Haber Haber { get; set; }
        public int Haber_Id { get; set; }
    }

    public class Haber
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Detay { get; set; }
        public string HaberResim { get; set; }
        //public Kategori Kategori { get; set; }
        public int KategoriId { get; set; }
        public string KategorininAdi { get; set; }
        [NotMapped]
        public List<SelectListItem> KategoriList { get; set; }

        public ICollection<Yorum> HaberYorumlar { get; set; }
    }
}