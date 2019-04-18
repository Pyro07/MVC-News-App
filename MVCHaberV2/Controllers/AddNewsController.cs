 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCHaberV2.Models;

namespace MVCHaberV2.Controllers
{
    public class AddNewsController : Controller
    {
        // GET: AddNews
        [Route("panel-tüm-haberler")]
        [Authorize]
        public ActionResult Index()
        {
            using (HaberDB dataBase = new HaberDB())
            {
                IndexModel model = new IndexModel
                {
                    IMKategoriler = dataBase.Kategoriler.ToList(),
                    IMHaberler = dataBase.Haberler.ToList()
                };
                return View(model);
            }
        }

        [Route("kategori-ekle")]
        [Authorize]
        public ActionResult KategoriEkle()
        {
            using (HaberDB dataBase = new HaberDB())
            {
                ViewBag.kategoriler = dataBase.Kategoriler.ToList();
            }
            return View();
        }

        [Route("kategori-ekle")]
        [HttpPost]
        [Authorize]
        public ActionResult KategoriEkle(Kategori model)
        {
            using (HaberDB dataBase = new HaberDB())
            {
                //var item = dataBase.Kategoriler.Where(k => k.KategoriAdi == model.KategoriAdi).FirstOrDefault();
                //if (item == null)
                //{
                //    Kategori kategori = new Kategori();
                //    kategori.KategoriAdi = model.KategoriAdi;
                //    dataBase.Kategoriler.Add(kategori);
                //    dataBase.SaveChanges();
                //    ViewBag.Sonuc = "Kategori başarı ile eklendi";
                //    ViewBag.kategoriler = dataBase.Kategoriler.ToList();
                //    return View(model);
                //}
                //else
                //{
                //    ViewBag.Hata = "Eklemeye çalıştığınız kategori bulunmaktadır, lütfen başka bir kategori ekleyiniz.";
                //    ViewBag.kategoriler = dataBase.Kategoriler.ToList();
                //    return View();
                //}

                if (dataBase.Kategoriler.Count(k => k.KategoriAdi == model.KategoriAdi) > 0)
                {
                    ViewBag.Hata = "Eklemeye çalıştığınız kategori bulunmaktadır, lütfen başka bir kategori ekleyiniz";
                    ViewBag.kategoriler = dataBase.Kategoriler.ToList();
                    ModelState.Clear();
                }
                else
                {
                    Kategori kategori = new Kategori();
                    kategori.KategoriAdi = model.KategoriAdi;
                    dataBase.Kategoriler.Add(kategori);
                    dataBase.SaveChanges();
                    ViewBag.Sonuc = "Kategori başarı ile eklendi";
                    ViewBag.kategoriler = dataBase.Kategoriler.ToList();
                }
                return View(model);
            }
        }
        [Authorize]
        public ActionResult KategoriSil(int? id)
        {
            using (HaberDB dataBase = new HaberDB())
            {
                //if (dataBase.Kategoriler.Count(k => k.Id == id) > 0)
                //{
                //    //return RedirectToAction("KategoriEkle/1");
                //    return RedirectToAction("HaberEkle/1");
                //}
                //else
                //{
                //    Kategori kategori = dataBase.Kategoriler.Where(k => k.Id == id).SingleOrDefault();
                //    dataBase.Kategoriler.Remove(kategori);
                //    dataBase.SaveChanges();
                //    return RedirectToAction("HaberEkle");
                //}
                if (dataBase.Haberler.Count(k => k.KategoriId == id) > 0)
                {
                    return View("HaberEkle/1");
                }
                else
                {
                    Kategori kategori = dataBase.Kategoriler.Where(k => k.Id == id).SingleOrDefault();
                    dataBase.Kategoriler.Remove(kategori);
                    dataBase.SaveChanges();
                    ViewBag.kategoriler = dataBase.Kategoriler.ToList();
                    return View("HaberEkle");
                }
            }
        }

        [Route("haber-ekle")]
        [Authorize]
        public ActionResult HaberEkle(int? id)
        {
            using (HaberDB dataBase = new HaberDB())
            {
                if (id == 1)
                {
                    ViewBag.HataSil = "Silmeye çalıştığınız kategoride haber bulunduğu için silinemiyor.";
                }

                Haber model = KategoriDoldur();
                ViewBag.kategoriler = dataBase.Kategoriler.ToList();
                return View(model);
            }
        }

        [Route("haber-ekle")]
        [HttpPost][Authorize]
        public ActionResult HaberEkle(Haber model)
        {
            using (HaberDB dataBase = new HaberDB())
            {
                Haber haber = new Haber();
                haber.Baslik = model.Baslik;
                haber.Detay = model.Detay;

                if (Request.Files.Count > 0)
                {
                    string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string TamYolYeri = "~/Images/KullaniciResimleri/" + DosyaAdi + uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                    model.HaberResim = DosyaAdi + uzanti;
                    haber.HaberResim = model.HaberResim;
                }

                haber.KategoriId = model.KategoriId;
                model.KategoriList = KategoriDoldur().KategoriList;
                var secilenKategori = model.KategoriList.Find(k => k.Value == model.KategoriId.ToString());
                haber.KategorininAdi = secilenKategori.Text;

                dataBase.Haberler.Add(haber);
                dataBase.SaveChanges();
                ViewBag.kategoriler = dataBase.Kategoriler.ToList();
                return View(model);
            }
        }
        [Authorize]
        public ActionResult HaberSil(int? id)
        {
            using (HaberDB dataBase = new HaberDB())
            {
                Haber haber = dataBase.Haberler.Where(h => h.Id == id).SingleOrDefault();
                dataBase.Haberler.Remove(haber);
                dataBase.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        private static Haber KategoriDoldur()
        {
            using (HaberDB dataBase = new HaberDB())
            {
                Haber model = new Haber
                {
                    KategoriList = (from kategori in dataBase.Kategoriler.ToList()
                                    select new SelectListItem
                                    {
                                        Selected = false,
                                        Text = kategori.KategoriAdi,
                                        Value = kategori.Id.ToString()
                                    }).ToList()
                };
                model.KategoriList.Insert(0, new SelectListItem
                {
                    Selected = true,
                    Value = "",
                    Text = "Seçiniz",
                });
                return model;
            }
        }

        [Route("haber-guncelle")]
        [Authorize]
        public ActionResult HaberGuncelle(int? id)
        {
            using (HaberDB dataBase = new HaberDB())
            {
                Haber haber = dataBase.Haberler.Where(h => h.Id == id).SingleOrDefault();
                Haber model = new Haber();
                model.Baslik = haber.Baslik;
                model.Detay = haber.Detay;
                if (Request.Files.Count > 0)
                {
                    string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string tamYolYeri = "~/Images/KullaniciResimleri/" + dosyaAdi + uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(tamYolYeri));
                    model.HaberResim = dosyaAdi + uzanti;
                    haber.HaberResim = model.HaberResim;
                }
                model.KategoriId = haber.KategoriId;
                model.KategoriList = KategoriDoldur().KategoriList;
                var secilenKategori = model.KategoriList.Find(k => k.Value == model.KategoriId.ToString());
                haber.KategorininAdi = secilenKategori.Text;

                ViewBag.kategoriler = dataBase.Kategoriler.ToList();
                return View(model);
            }
        }

        [Route("haber-guncelle")]
        [HttpPost]
        [Authorize]
        public ActionResult HaberGuncelle(Haber model)
        {
            using (HaberDB dataBase = new HaberDB())
            {
                Haber haber = dataBase.Haberler.Where(h => h.Id == model.Id).SingleOrDefault();

                haber.Baslik = model.Baslik;
                haber.Detay = model.Detay;
                if (Request.Files.Count > 0) 
                {
                    string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string tamYolYeri = "~/Images/KullaniciResimleri/" + dosyaAdi + uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(tamYolYeri));
                    model.HaberResim = dosyaAdi + uzanti;
                    haber.HaberResim = model.HaberResim;
                }
                haber.KategoriId = model.KategoriId;
                model.KategoriList = KategoriDoldur().KategoriList;
                var secilenKategori = model.KategoriList.Find(k => k.Value == model.KategoriId.ToString());
                haber.KategorininAdi = secilenKategori.Text;
                dataBase.SaveChanges();
                ViewBag.kategoriler = dataBase.Kategoriler.ToList();
                return View();
            }
        }
    }
}