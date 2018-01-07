using ETDS.Models;
using EveTeslimDiyetServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EveTeslimDiyetServis.Controllers
{
    public class HomeController : Controller
    {
        ETDSContext cont = new ETDSContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Kullanici kullanici, string SifreTekrar, string EpostaTekrar)
        {
            if (kullanici.EPosta != EpostaTekrar)
            {
                ViewBag.Error = "Girilen E-Posta adresileri eşleşmiyor.";
                return View("Register", "", kullanici);
            }
            if (kullanici.Sifre != SifreTekrar)
            {
                ViewBag.Error = "Girilen şifreler eşleşmiyor.";
                return View(kullanici);
            }
            if (kullanici.Sifre == SifreTekrar && kullanici.EPosta == EpostaTekrar)
            {
                kullanici.RolId = (int)EnumRol.Kullanici;
                cont.Kullanici.Add(kullanici);
                cont.SaveChanges();
                ViewBag.Success = "İşleminiz başarıyla gerçekleştirilmiştir. Sisteme giriş yapabilirisiniz.";
                return View();
            }
            return View();
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
        public ActionResult Prizes()
        {
            return View();
        }
    }
}