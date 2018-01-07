using ETDS.Models;
using EveTeslimDiyetServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EveTeslimDiyetServis.Controllers
{
    public class AccountController : Controller
    {
        //Sistem kullanıcılarının sisteme giriş yaptığı bölümdür.
        ETDSContext cont = new ETDSContext();

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciEntity = cont.Kullanici.Where(u => u.KullaniciAd == kullanici.KullaniciAd && u.Sifre == kullanici.Sifre).FirstOrDefault();
            if (kullaniciEntity != null)
            {
                //Kullanıcının Rolune bağlı olarak ilgili role ait sayfalara yönlendirme yapıyoruz.
                if (kullaniciEntity.RolId == (int)EnumRol.Admin)
                {
                    Session["kullanici_adi"] = kullaniciEntity.KullaniciAd;
                    return RedirectToAction("ListKullanici", "SistemKullanici");
                }
               else if (kullaniciEntity.RolId == (int)EnumRol.Kullanici)
                {
                    Session["kullanici_adi"] = kullaniciEntity.KullaniciAd;
                    return RedirectToAction("Index", "Kullanici");
                }
                else if (kullaniciEntity.RolId == (int)EnumRol.Diyetisyen)
                {
                    Session["kullanici_adi"] = kullaniciEntity.KullaniciAd;
                    return RedirectToAction("ListMenu", "Diyetisyen");
                }
                else if (kullaniciEntity.RolId == (int)EnumRol.Asci)
                {
                    Session["kullanici_adi"] = kullaniciEntity.KullaniciAd;
                    return RedirectToAction("ListSiparisAlinan", "Asci");
                }
                else if (kullaniciEntity.RolId == (int)EnumRol.Kurye)
                {
                    Session["kullanici_adi"] = kullaniciEntity.KullaniciAd;
                    return RedirectToAction("ListSiparisAlinan", "Kurye");
                }
                return View("Index");

            }
            else
            {
                ViewBag.Error = "Geçersiz Kullanıcı Adı veya Şifre ..";
                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            //oturumu kaldır
            Session.Remove("kullanici_adi");
            return View("Index");
        }

    }
}