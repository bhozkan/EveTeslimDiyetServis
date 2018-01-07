using ETDS.Models;
using EveTeslimDiyetServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EveTeslimDiyetServis.Controllers
{
    public class KuryeController : Controller
    {
        ETDSContext cont = new ETDSContext();
        // GET: Kurye
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListSiparisAlinan()
        {
            var kullaniciAdi = (string)Session["kullanici_adi"];
            var kullanici = cont.Kullanici.Where(l => l.KullaniciAd.Equals(kullaniciAdi)).FirstOrDefault();
            // Sisteme giriş yapan kuryenin bulundugu ilde gelen siparişleri filtreliyoruz.
            var siparisList = cont.Siparis.Where(l => l.Kullanici.IlId == kullanici.IlId && l.DurumId == (int)EnumSiparisDurum.SiparisDagitimda).ToList().OrderByDescending(l => l.Tarih);
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
            siparisEdit.DurumId = (int)EnumSiparisDurum.SiparisOnaylandi;
            cont.SaveChanges();
            return RedirectToAction("ListSiparisAlinan");
        }


        //Kuryeye gönderilen siparişlerin listelendiği ekrandır.
        public ActionResult ListSiparisTamamlanan()
        {
            var kullaniciAdi = (string)Session["kullanici_adi"];
            var kullanici = cont.Kullanici.Where(l => l.KullaniciAd.Equals(kullaniciAdi)).FirstOrDefault();
            // Sisteme giriş yapan kuryenin bulundugu ilde gelen siparişleri filtreliyoruz.
            var siparisList = cont.Siparis.Where(l => l.Kullanici.IlId == kullanici.IlId && l.DurumId == (int)EnumSiparisDurum.SiparisOnaylandi).ToList().OrderByDescending(l => l.Tarih);
            return View(siparisList);
        }

    }
}