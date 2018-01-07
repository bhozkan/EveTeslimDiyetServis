using ETDS.Models;
using EveTeslimDiyetServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EveTeslimDiyetServis.Controllers
{
    public class DiyetisyenController : Controller
    {

        ETDSContext cont = new ETDSContext();
        // GET: Diyetisyen
        public ActionResult Index()
        {
            return View();
        }

        //Sistemde yer alan menulerin listelendiği action
        public ActionResult ListMenu()
        {
            var menuList = cont.Menu.ToList();
            return View(menuList);
        }

        //Sisteme yeni menu eklemek için kullanığımız action method
        public ActionResult CreateMenu()
        {
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
            return View();
        }
        [HttpPost]
        public ActionResult CreateMenu(Menu menu)
        {
           //Sisteme yalnızca diyetisyen roluyle giren kullanıcı bu işlemi yapabildiğinden kullanıcıdan DiyetisyenId alanına erişiyoruz.
            var kullaniciAdi= (string)Session["kullanici_adi"];
            var kullaniciId = cont.Kullanici.Where(l => l.KullaniciAd.Equals(kullaniciAdi)).Select(l=>l.Id).FirstOrDefault();
            var diyetisyenId = cont.Diyetisyen.Where(l => l.KullaniciId == kullaniciId).Select(l => l.Id).FirstOrDefault();
            menu.DiyetisyenId = diyetisyenId;
            //Menu oluşturulma tarihihi kaydın eklenme tarihi olarak atıyoruz.
            menu.MenuTarih = DateTime.Now;
            cont.Menu.Add(menu);
            cont.SaveChanges();
            return RedirectToAction("ListMenu", "Diyetisyen");

        }

        //Sistemde yer alan bir menulerın duzenlendigi action
        public ActionResult EditMenu(int id)
        {
            var menu = cont.Menu.Find(id);
            var paketList = cont.Paket.ToList().Select(k => new SelectListItem
            {
                Selected = true,
                Text = k.Aciklama,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.PaketList = paketList;

            var ogunList = cont.Ogun.ToList().Select(k => new SelectListItem
            {
                Selected = true,
                Text = k.Aciklama,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.OgunList = ogunList;
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMenu(Menu menu)
        {
            var menuEdit = cont.Menu.Find(menu.Id);
            if (menuEdit != null)
            {
                menuEdit.Ad = menu.Ad;
                menuEdit.Aciklama = menu.Aciklama;
                menuEdit.DiyetisyenId = menu.DiyetisyenId;
                menuEdit.OgunId = menu.OgunId;
                menuEdit.PaketId = menu.PaketId;
                menuEdit.MenuTarih = menu.MenuTarih;
                cont.SaveChanges();
                return RedirectToAction("ListMenu", "Diyetisyen", new { id = menuEdit.Id });

            }
            return View();
        }


        //-----Menu silmek icin kullandigimiz action 
        public ActionResult DeleteMenu(int Id)
        {
            var menu = cont.Menu.Where(l => l.Id == Id).FirstOrDefault();
            return PartialView(menu);
        }

        [HttpPost, ActionName("DeleteMenu")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMenuOnay(Menu menu)
        {
            var menuSil = cont.Menu.Where(l => l.Id == menu.Id).FirstOrDefault();
            cont.Menu.Remove(menuSil);
            cont.SaveChanges();
            return RedirectToAction("ListMenu", "Diyetisyen");
        }

        //Sistemde yer alan yemeklerin listelendiği action
        public ActionResult ListYemek()
        {
            var yemekList = cont.Yemek.ToList();
            return View(yemekList);
        }

        //Yemek eklemek icin kullandigimiz action
        [HttpPost]
        public ActionResult YemekEkle(Yemek yemek)
        {
            cont.Yemek.Add(yemek);
            cont.SaveChanges();
            return RedirectToAction("ListYemek", "Diyetisyen");

        }
        //--Yemek duzenlemenin yapildigi action
        public ActionResult EditYemek(int id)
        {
            var yemek = cont.Yemek.Find(id);
            return PartialView(yemek);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditYemek(Yemek yemek)
        {
           var yemekEdit =cont.Yemek.Where(l=>l.Id==yemek.Id).FirstOrDefault();
            yemekEdit.Ad = yemek.Ad;
            yemekEdit.Aciklama = yemek.Aciklama;
            cont.SaveChanges();
            return RedirectToAction("ListYemek", "Diyetisyen");

        }

        //--Yemek silmek icin kullandigimiz action 
        public ActionResult DeleteYemek(int Id)
        {
            var yemek = cont.Yemek.Where(l => l.Id == Id).FirstOrDefault();
            return PartialView(yemek);
        }

        [HttpPost, ActionName("DeleteYemek")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteYemekOnay(Yemek yemek)
        {
            var yemekSil = cont.Yemek.Where(l => l.Id == yemek.Id).FirstOrDefault();
            cont.Yemek.Remove(yemekSil);
            cont.SaveChanges();
            return RedirectToAction("ListYemek", "Diyetisyen");
        }

        //Sistemde yer alan yemeklerin listelendiği action
        public ActionResult MenuIcerik(int Id)
        {
            //Sisteme yalnızca diyetisyen roluyle giren kullanıcı bu işlemi yapabildiğinden kullanıcıdan DiyetisyenId alanına erişiyoruz.
            var kullaniciAdi = (string)Session["kullanici_adi"];
            var kullaniciId = cont.Kullanici.Where(l => l.KullaniciAd.Equals(kullaniciAdi)).Select(l => l.Id).FirstOrDefault();
            var diyetisyenId = cont.Diyetisyen.Where(l => l.KullaniciId == kullaniciId).Select(l => l.Id).FirstOrDefault();
            //Seçilen menuye ait yemek bilgilerini çekiyoruz.
            var yemekList = cont.YemekMenu.Where(l=>l.MenuId==Id).ToList();
            ViewBag.MenuId = Id;
            ViewBag.MenuAd = cont.Menu.Where(l => l.Id == Id).Select(l => l.Ad).FirstOrDefault();
            return View(yemekList);
        }

        public ActionResult IcerikYemekEkle(int Id)
        {
            var yemekList = cont.Yemek.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.YemekIcerikList = yemekList;
            ViewBag.MenuId = Id;
            return PartialView("IcerikYemekEkle");

        }
        [HttpPost]
        public ActionResult IcerikYemekEkle(YemekMenu yemekMenu)
        {
            cont.YemekMenu.Add(yemekMenu);
            cont.SaveChanges();
            return RedirectToAction("MenuIcerik", "Diyetisyen", new { id = yemekMenu.MenuId });

        }









    }
}