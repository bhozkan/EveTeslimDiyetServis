using ETDS.Models;
using EveTeslimDiyetServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EveTeslimDiyetServis.Controllers
{
    public class KullaniciController : Controller
    {
        ETDSContext cont = new ETDSContext();

        public ActionResult HomePage()
        {
            return View();
        }

        // GET: Kullanici
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Prizes()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        // GET: Kullanici
        public ActionResult Profil()
        {
            var kullaniciAdi = (string)Session["kullanici_adi"];
            var kullanici = cont.Kullanici.Where(l => l.KullaniciAd.Equals(kullaniciAdi)).FirstOrDefault();
            return View(kullanici);
        }

        // GET: Kullanici
        public ActionResult EditProfil()
        {
            var kullaniciAdi = (string)Session["kullanici_adi"];
            var kullanici = cont.Kullanici.Where(l => l.KullaniciAd.Equals(kullaniciAdi)).FirstOrDefault();
            var ilList = cont.Il.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.IlList = ilList;
            return View(kullanici);
        }

        [HttpPost]
        public ActionResult EditProfil(Kullanici kullanici, HttpPostedFileBase ProfileImage)
        {
            if (ProfileImage != null &&
                (ProfileImage.ContentType == "image/jpeg" ||
                    ProfileImage.ContentType == "image/jpg" ||
                    ProfileImage.ContentType == "image/png"))
            {
                string filename = $"user_{kullanici.Id}.{ProfileImage.ContentType.Split('/')[1]}";

                ProfileImage.SaveAs(Server.MapPath($"~/Images/{filename}"));
                kullanici.ProfilResmi = filename;
            }

            var kullaniciEdit = cont.Kullanici.Find(kullanici.Id);
            if (kullaniciEdit != null)
            {
                kullaniciEdit.Ad = kullanici.Ad;
                kullaniciEdit.Soyad = kullanici.Soyad;
                kullaniciEdit.KullaniciAd = kullanici.KullaniciAd;
                kullaniciEdit.Adres = kullanici.Adres;
                kullaniciEdit.Boy = kullanici.Boy;
                kullaniciEdit.Cinsiyet = kullanici.Cinsiyet;
                kullaniciEdit.EPosta = kullanici.EPosta;
                kullaniciEdit.Gsm = kullanici.Gsm;
                kullaniciEdit.IlId = kullanici.IlId;
                kullaniciEdit.Kilo = kullanici.Kilo;
                kullaniciEdit.ProfilResmi = kullanici.ProfilResmi;
            }

            cont.SaveChanges();
            var ilList = cont.Il.ToList().Select(k => new SelectListItem
            {
                Selected = true,
                Text = k.Ad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.IlList = ilList;
            ViewBag.Success = "İşleminiz başarıyla gerçekleştirilmiştir.";
            return View(kullanici);
        }

        public ActionResult CreateSiparis()
        {
            var kullaniciAdi = (string)Session["kullanici_adi"];
            var kullanici = cont.Kullanici.Where(l => l.KullaniciAd.Equals(kullaniciAdi)).FirstOrDefault();

            var paketList = cont.Paket.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Aciklama,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.PaketList = paketList;

            var ogunList = cont.Ogun.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Aciklama,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.OgunList = ogunList;
            SelectListItem adres = new SelectListItem();
            adres.Selected = false;
            adres.Text = kullanici.Adres;
            adres.Value = kullanici.Adres;
            var adresList = new List<SelectListItem>();
            adresList.Add(adres);
            ViewBag.AdresList = adresList;

            Siparis siparis = new Siparis();
            siparis.KullaniciId = kullanici.Id;
            return View(siparis);
        }

        [HttpPost]
        public ActionResult CreateSiparis(Siparis siparis)
        {
            var kullaniciAdi = (string)Session["kullanici_adi"];
            var kullanici = cont.Kullanici.Where(l => l.KullaniciAd.Equals(kullaniciAdi)).FirstOrDefault();

            var paketList = cont.Paket.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Aciklama,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.PaketList = paketList;

            var ogunList = cont.Ogun.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Aciklama,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.OgunList = ogunList;
            SelectListItem adres = new SelectListItem();
            adres.Selected = false;
            adres.Text = kullanici.Adres;
            adres.Value = kullanici.Adres;
            var adresList = new List<SelectListItem>();
            adresList.Add(adres);
            ViewBag.AdresList = adresList;

            siparis.Tarih = DateTime.Now;
            siparis.DurumId = (int)EnumSiparisDurum.SiparisAlindi;
            cont.Siparis.Add(siparis);
            cont.SaveChanges();
            ViewBag.Success = "Siparişiniz alınmıştır. Siparişlerim bölmünden kontrolünü sağlayabilirsiniz.";
            return View(siparis);
        }

        public ActionResult ListSiparis()
        {
            var kullaniciAdi = (string)Session["kullanici_adi"];
            var kullanici = cont.Kullanici.Where(l => l.KullaniciAd.Equals(kullaniciAdi)).FirstOrDefault();

            var siparisList = cont.Siparis.Where(l => l.KullaniciId == kullanici.Id).ToList();
            return View(siparisList);
        }

        public ActionResult GunlukKalori()
        {
            return View();
        }
        public ActionResult IdealKilo()
        {
            return View();
        }

        public ActionResult BelRisk()
        {
            return View();
        }


        public ActionResult BelKalca()
        {
            return View();
        }

        public ActionResult VucutKitle()
        {
            return View();
        }

    }
}