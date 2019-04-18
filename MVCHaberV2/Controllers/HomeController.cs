using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCHaberV2.Models;

namespace MVCHaberV2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Route("haberler/tum-haberler")]
        public ActionResult Index()
        {
            using (HaberDB dataBase = new HaberDB())
            {
                IndexModel model = new IndexModel
                {
                    IMKategoriler = dataBase.Kategoriler.ToList()
                };
                return View(model);
            }
        }

        [Route("haberler/gundem-haberleri")]
        public ActionResult Gündem()
        {
            using (HaberDB dataBase = new HaberDB())
            {
                IndexModel model = new IndexModel
                {
                    IMHaberler = dataBase.Haberler.ToList(),
                    IMKategoriler = dataBase.Kategoriler.ToList(),
                    IMYorumlar = dataBase.Yorumlar.ToList()
                };
                var item = model.IMHaberler.Where(h => h.KategorininAdi == "Gündem").ToList();
                model.IMHaberler = item;
                return View(model);
            }
        }

        [Route("haberler/ekonomi-haberleri")]
        public ActionResult Ekonomi()
        {
            using (HaberDB dataBase = new HaberDB())
            {
                IndexModel model = new IndexModel
                {
                    IMHaberler=dataBase.Haberler.ToList(),
                    IMKategoriler=dataBase.Kategoriler.ToList(),
                    IMYorumlar=dataBase.Yorumlar.ToList()
                };
                var item = model.IMHaberler.Where(h => h.KategorininAdi == "Ekonomi").ToList();
                model.IMHaberler = item;
                return View(model);
            }
        }

        [Route("haberler/dunya-haberleri")]
        public ActionResult Dünya()
        {
            using (HaberDB dataBase = new HaberDB())
            {
                IndexModel model = new IndexModel
                {
                    IMHaberler = dataBase.Haberler.ToList(),
                    IMKategoriler = dataBase.Kategoriler.ToList()
                };
                var item = model.IMHaberler.Where(h => h.KategorininAdi == "Dünya").ToList();
                model.IMHaberler = item;
                return View(model);
            }
        }

        [Route("haberler/egitim-haberleri")]
        public ActionResult Eğitim()
        {
            using (HaberDB dataBase = new HaberDB())
            {
                IndexModel model = new IndexModel
                {
                    IMHaberler = dataBase.Haberler.ToList(),
                    IMKategoriler = dataBase.Kategoriler.ToList()
                };
                var item = model.IMHaberler.Where(h => h.KategorininAdi == "Eğitim").ToList();
                model.IMHaberler = item;
                return View(model);
            }
        }

        [Route("haberler/spor-haberleri")]
        public ActionResult Spor()
        {
            using (HaberDB dataBase = new HaberDB())
            {
                IndexModel model = new IndexModel
                {
                    IMHaberler = dataBase.Haberler.ToList(),
                    IMKategoriler = dataBase.Kategoriler.ToList()
                };
                var item = model.IMHaberler.Where(h => h.KategorininAdi == "Spor").ToList();
                model.IMHaberler = item;
                return View(model);
            }
        }

        [Route("haberler/magazin-haberleri")]
        public ActionResult Magazin()
        {
            using (HaberDB dataBase = new HaberDB())
            {
                IndexModel model = new IndexModel
                {
                    IMHaberler = dataBase.Haberler.ToList(),
                    IMKategoriler = dataBase.Kategoriler.ToList()
                };
                var item = model.IMHaberler.Where(h => h.KategorininAdi == "Magazin").ToList();
                model.IMHaberler = item;
                return View(model);
            }
        }

        public ActionResult YorumEkle(string yorum, int? id,string SecKategori)
        {
            using (HaberDB dataBase = new HaberDB())
            {
                IndexModel model = new IndexModel
                {
                    IMHaberler = dataBase.Haberler.ToList(),
                    IMKategoriler = dataBase.Kategoriler.ToList()
                };

                var secilenHaber = model.IMHaberler.Where(h => h.Id == id).SingleOrDefault();
                Yorum yeniYorum = new Yorum
                {
                    Icerik = yorum,
                    Tarih = DateTime.Now,
                    Haber_Id = secilenHaber.Id
                };
                dataBase.Yorumlar.Add(yeniYorum);
                dataBase.SaveChanges();

                //haber.HaberYorumlar = new List<Yorum>();
                //haber.HaberYorumlar.Add(yeniYorum);
                
                model.IMYorumlar = dataBase.Yorumlar.ToList();
                var secilenKategori = model.IMHaberler.Find(k => k.KategorininAdi == SecKategori);
                return RedirectToAction(secilenKategori.KategorininAdi);
            }
        }
    }
}