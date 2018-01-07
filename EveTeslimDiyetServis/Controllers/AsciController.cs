using ETDS.Models;
using EveTeslimDiyetServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EveTeslimDiyetServis.Controllers
{
    public class AsciController : Controller
    {
        ETDSContext cont = new ETDSContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        // GET: Menu
        public ActionResult ListSiparisAlinan()
        {
            var kullaniciAdi = (string)Session["kullanici_adi"];
            var kullanici = cont.Kullanici.Where(l => l.KullaniciAd.Equals(kullaniciAdi)).FirstOrDefault();
            // Sisteme giriş yapan aşçının bulundugu ilde gelen siparişleri filtreliyoruz.
            var siparisList = cont.Siparis.Where(l => l.Kullanici.IlId == kullanici.IlId && l.DurumId !=(int)EnumSiparisDurum.SiparisOnaylandi).ToList().OrderByDescending(l=>l.Tarih);
            return View(siparisList);
        }
        //Kullanıcı tarafında siparis durum bilgisinin guncellenmesi için eklenmiştir.
        public ActionResult SiparisOnay(int Id)
        {
            var siparis = cont.Siparis.Find(Id);
            return PartialView(siparis);
        }

        [HttpPost]
        public ActionResult SiparisOnay(Siparis siparis)
        {
            var siparisEdit = cont.Siparis.Find(siparis.Id);
            siparisEdit.DurumId = (int)EnumSiparisDurum.SiparisHazirlaniyor;
            cont.SaveChanges();
            return RedirectToAction("ListSiparisAlinan");
        }

        public ActionResult SiparisGonder(int Id)
        {
            var siparis = cont.Siparis.Find(Id);
            var kullaniciAdi = (string)Session["kullanici_adi"];
            var kullanici = cont.Kullanici.Where(l => l.KullaniciAd.Equals(kullaniciAdi)).FirstOrDefault();

            var siparisList = cont.Siparis.ToList().Select(k => new SelectListItem
            {
                Selected = true,
                Text = k.Id.ToString(),
                Value = k.Id.ToString()
            }).ToList();         
            ViewBag.SiparisList = siparisList;
            //Aşçının bulundugu ilde bulunan kryelerin listelenmesini sağlıyoruz.
            var kuryeList = cont.Kurye.Where(l=>l.Kullanici.IlId==kullanici.IlId).ToList().Select(k => new SelectListItem
            {
                Selected = true,
                Text = k.Kullanici.Ad.ToString()+" "+ k.Kullanici.Soyad.ToString(),
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.KuryeList = kuryeList;
            return View(siparis);
        }

        [HttpPost]
        public ActionResult SiparisGonder(Siparis siparis)
        {
            var siparisEdit = cont.Siparis.Find(siparis.Id);
            siparisEdit.DurumId = (int)EnumSiparisDurum.SiparisDagitimda;
            cont.SaveChanges();
            return RedirectToAction("ListSiparisAlinan");
        }

        //Kuryeye gönderilen siparişlerin listelendiği ekrandır.
        public ActionResult ListSiparisTamamlanan()
        {
            var kullaniciAdi = (string)Session["kullanici_adi"];
            var kullanici = cont.Kullanici.Where(l => l.KullaniciAd.Equals(kullaniciAdi)).FirstOrDefault();
            // Sisteme giriş yapan aşçının bulundugu ilde gelen siparişleri filtreliyoruz.
            var siparisList = cont.Siparis.Where(l => l.Kullanici.IlId == kullanici.IlId && l.DurumId ==(int)EnumSiparisDurum.SiparisDagitimda).ToList().OrderByDescending(l => l.Tarih);
            return View(siparisList);
        }


        //Kuryeye gönderilen siparişlerin listelendiği ekrandır.
        public ActionResult ListMenu()
        {
            var kullaniciAdi = (string)Session["kullanici_adi"];
            var kullanici = cont.Kullanici.Where(l => l.KullaniciAd.Equals(kullaniciAdi)).FirstOrDefault();
            // Sisteme giriş yapan aşçının bulundugu ilde gelen menuleri filtreliyoruz.
            var menuList = cont.Menu.Where(l => l.Diyetisyen.Kullanici.IlId == kullanici.IlId).ToList().OrderByDescending(l => l.MenuTarih);
            return View(menuList);
        }

        //Ascinin menulerin içeriğini goruntuledıgı actiondır.
        public ActionResult MenuIcerik(int Id)
        {
            ViewBag.MenuId = Id;
            ViewBag.MenuAd = cont.Menu.Where(l => l.Id == Id).Select(l => l.Ad).FirstOrDefault();
            var yemekList = cont.YemekMenu.Where(l => l.MenuId == Id).ToList();
            return View(yemekList);
        }

    }
}