using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETDS.Models;
using EveTeslimDiyetServis.Models;

namespace EveTeslimDiyetServis.Controllers
{
    public class SistemKullaniciController : Controller
    {

        ETDSContext cont = new ETDSContext();
        // GET: SistemKullanici
        public ActionResult Admin()
        {
            return View();
        }

        //Sistemde yer alan kullanıcıların listelendiği action
        public ActionResult ListKullanici()
        {
            var kullaniciList = cont.Kullanici.ToList();
            return View(kullaniciList);
        }

        //Sisteme yeni kullanıcı eklemek için kullanığımız action method
        public ActionResult CreateKullanici()
        {
            var rolList = cont.Rol.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.RolList = rolList;
            var ilList = cont.Il.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.IlList = ilList;
            return View();
        }
        [HttpPost]
        public ActionResult CreateKullanici(Kullanici kullanici)
        {
            cont.Kullanici.Add(kullanici);
            cont.SaveChanges();
            return RedirectToAction("ListKullanici", "SistemKullanici");

        }

        //Sistemde yer alan bir kullanıcının duzenlendigi action
        public ActionResult EditKullanici(int id)
        {
            var rolList = cont.Rol.ToList().Select(k => new SelectListItem
            {
                Selected = true,
                Text = k.Ad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.RolListEdit = rolList;
            var ilList = cont.Il.ToList().Select(k => new SelectListItem
            {
                Selected = true,
                Text = k.Ad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.IlListEdit = ilList;
            var kullanici = cont.Kullanici.Find(id);
            return View(kullanici);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditKullanici(Kullanici kullanici)
        {
            var kullaniciEdit = cont.Kullanici.Find(kullanici.Id);
            if (kullaniciEdit != null)
            {
                kullaniciEdit.Ad = kullanici.Ad;
                kullaniciEdit.Soyad = kullanici.Soyad;
                kullaniciEdit.KullaniciAd = kullanici.KullaniciAd;
                kullaniciEdit.Sifre = kullanici.Sifre;
                kullaniciEdit.Cinsiyet = kullanici.Cinsiyet;
                kullaniciEdit.EPosta = kullanici.EPosta;
                kullaniciEdit.Gsm = kullanici.Gsm;
                kullaniciEdit.IlId = kullanici.IlId;
                kullaniciEdit.RolId = kullanici.RolId;
                cont.SaveChanges();
                return RedirectToAction("ListKullanici", "SistemKullanici", new { id = kullaniciEdit.Id });

            }
            return View();
        }


        //-----Kullanıcı silmek icin kullandigimiz action 
        public ActionResult DeleteKullanici(int Id)
        {
            var kullanici = cont.Kullanici.Where(l => l.Id == Id).FirstOrDefault();
            return PartialView(kullanici);
        }

        [HttpPost, ActionName("DeleteKullanici")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteKullaniciOnay(Kullanici kullanici)
        {
            var kullaniciSil = cont.Kullanici.Where(l => l.Id == kullanici.Id).FirstOrDefault();
            cont.Kullanici.Remove(kullaniciSil);
            cont.SaveChanges();
            return RedirectToAction("ListKullanici", "SistemKullanici");
        }


        //Sistemde yer alan diyetisyenlerin listelendiği action
        public ActionResult ListDiyetisyen()
        {
            var diyetisyenList = cont.Diyetisyen.ToList();
            return View(diyetisyenList);
        }

        //Sisteme yeni diyetisyen eklemek için kullanığımız action method
        public ActionResult CreateDiyetisyen()
        {
            var rolList = cont.Rol.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad,
                Value = k.Id.ToString()
            }).ToList();
            //Diyetisyen Rolunu selected olarak getiriyoruz.
            var rolDiyetisyen = rolList.Where(l => l.Value == Convert.ToString(3)).FirstOrDefault();
            rolDiyetisyen.Selected = true;
            ViewBag.RolList = rolList;

            var kullaniciList = cont.Kullanici.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad + " " + k.Soyad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.KullaniciList = kullaniciList;
            return View();
        }
        [HttpPost]
        public ActionResult CreateDiyetisyen(Diyetisyen diyetisyen)
        {
            var rolId = cont.Kullanici.Where(l => l.Id == diyetisyen.KullaniciId).Select(l => l.RolId).FirstOrDefault();
            if (rolId != 3)
            {
                ViewBag.Error = "Rolü diyetisyen olan bir kullanıcı seçmeniz gerekmektedir.";
                var rolList = cont.Rol.ToList().Select(k => new SelectListItem
                {
                    Selected = false,
                    Text = k.Ad,
                    Value = k.Id.ToString()
                }).ToList();
                //Diyetisyen Rolunu selected olarak getiriyoruz.
                var rolDiyetisyen = rolList.Where(l => l.Value == Convert.ToString(3)).FirstOrDefault();
                rolDiyetisyen.Selected = true;
                ViewBag.RolList = rolList;

                var kullaniciList = cont.Kullanici.ToList().Select(k => new SelectListItem
                {
                    Selected = false,
                    Text = k.Ad + " " + k.Soyad,
                    Value = k.Id.ToString()
                }).ToList();
                ViewBag.KullaniciList = kullaniciList;
                return View(diyetisyen);

            }
            else
            {
                cont.Diyetisyen.Add(diyetisyen);
                cont.SaveChanges();
                return RedirectToAction("ListDiyetisyen", "SistemKullanici");
            }

        }

        //Sistemde yer alan bir kullanıcının duzenlendigi action
        public ActionResult EditDiyetisyen(int id)
        {
            var rolList = cont.Rol.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad,
                Value = k.Id.ToString()
            }).ToList();
            //Diyetisyen Rolunu selected olarak getiriyoruz.
            var rolDiyetisyen = rolList.Where(l => l.Value == Convert.ToString(3)).FirstOrDefault();
            rolDiyetisyen.Selected = true;
            ViewBag.RolList = rolList;

            var kullaniciList = cont.Kullanici.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad + " " + k.Soyad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.KullaniciList = kullaniciList;
            var diyetisyen = cont.Diyetisyen.Find(id);
            return View(diyetisyen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDiyetisyen(Diyetisyen diyetisyen)
        {
            var diyetisyenEdit = cont.Diyetisyen.Find(diyetisyen.Id);
            if (diyetisyenEdit != null)
            {
                if (diyetisyenEdit.Kullanici.RolId != 3)
                {
                    ViewBag.Error = "Rolü diyetisyen olan bir kullanıcı seçmeniz gerekmektedir.";
                    var rolList = cont.Rol.ToList().Select(k => new SelectListItem
                    {
                        Selected = true,
                        Text = k.Ad,
                        Value = k.Id.ToString()
                    }).ToList();
                    //Diyetisyen Rolunu selected olarak getiriyoruz.
                    var rolDiyetisyen = rolList.Where(l => l.Value == Convert.ToString(3)).FirstOrDefault();
                    rolDiyetisyen.Selected = true;
                    ViewBag.RolList = rolList;

                    var kullaniciList = cont.Kullanici.ToList().Select(k => new SelectListItem
                    {
                        Selected = true,
                        Text = k.Ad + " " + k.Soyad,
                        Value = k.Id.ToString()
                    }).ToList();
                    ViewBag.KullaniciList = kullaniciList;
                    return View(diyetisyen);

                }
                else
                {
                    diyetisyenEdit.KullaniciId = diyetisyen.KullaniciId;
                    cont.SaveChanges();
                    return RedirectToAction("ListDiyetisyen", "SistemKullanici", new { id = diyetisyenEdit.Id });
                }
            }
            return View();
        }


        //-----Diyetisyeni silmek icin kullandigimiz action 
        public ActionResult DeleteDiyetisyen(int Id)
        {
            var diyetisyen = cont.Diyetisyen.Where(l => l.Id == Id).FirstOrDefault();
            return PartialView(diyetisyen);
        }

        [HttpPost, ActionName("DeleteDiyetisyen")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDiyetisyenOnay(Diyetisyen diyetisyen)
        {
            var diyetisyenSil = cont.Diyetisyen.Where(l => l.Id == diyetisyen.Id).FirstOrDefault();
            cont.Diyetisyen.Remove(diyetisyenSil);
            cont.SaveChanges();
            return RedirectToAction("ListDiyetisyen", "SistemKullanici");
        }



        //Sistemde yer alan ascilarin listelendiği action
        public ActionResult ListAsci()
        {
            var asciList = cont.Asci.ToList();
            return View(asciList);
        }

        //Sisteme yeni asci eklemek için kullanığımız action method
        public ActionResult CreateAsci()
        {
            var rolList = cont.Rol.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad,
                Value = k.Id.ToString()
            }).ToList();
            //Diyetisyen Rolunu selected olarak getiriyoruz.
            var rolAsci = rolList.Where(l => l.Value == Convert.ToString(4)).FirstOrDefault();
            rolAsci.Selected = true;
            ViewBag.RolList = rolList;

            var kullaniciList = cont.Kullanici.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad + " " + k.Soyad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.KullaniciList = kullaniciList;
            return View();
        }
        [HttpPost]
        public ActionResult CreateAsci(Asci asci)
        {
            var rolId = cont.Kullanici.Where(l => l.Id == asci.KullaniciId).Select(l => l.RolId).FirstOrDefault();
            if (rolId != 4)
            {
                ViewBag.Error = "Rolü aşçı olan bir kullanıcı seçmeniz gerekmektedir.";
                var rolList = cont.Rol.ToList().Select(k => new SelectListItem
                {
                    Selected = false,
                    Text = k.Ad,
                    Value = k.Id.ToString()
                }).ToList();
                //Diyetisyen Rolunu selected olarak getiriyoruz.
                var rolAsci = rolList.Where(l => l.Value == Convert.ToString(4)).FirstOrDefault();
                rolAsci.Selected = true;
                ViewBag.RolList = rolList;

                var kullaniciList = cont.Kullanici.ToList().Select(k => new SelectListItem
                {
                    Selected = false,
                    Text = k.Ad + " " + k.Soyad,
                    Value = k.Id.ToString()
                }).ToList();
                ViewBag.KullaniciList = kullaniciList;
                return View(asci);

            }
            else
            {
                cont.Asci.Add(asci);
                cont.SaveChanges();
                return RedirectToAction("ListAsci", "SistemKullanici");
            }

        }

        //Sistemde yer alan bir ascinin duzenlendigi action
        public ActionResult EditAsci(int id)
        {
            var rolList = cont.Rol.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad,
                Value = k.Id.ToString()
            }).ToList();
            //Diyetisyen Rolunu selected olarak getiriyoruz.
            var rolAsci = rolList.Where(l => l.Value == Convert.ToString(4)).FirstOrDefault();
            rolAsci.Selected = true;
            ViewBag.RolList = rolList;

            var kullaniciList = cont.Kullanici.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad + " " + k.Soyad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.KullaniciList = kullaniciList;
            var asci = cont.Asci.Find(id);
            return View(asci);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAsci(Asci asci)
        {
            var asciEdit = cont.Diyetisyen.Find(asci.Id);
            if (asciEdit != null)
            {
                if (asciEdit.Kullanici.RolId != 4)
                {
                    ViewBag.Error = "Rolü aşçı olan bir kullanıcı seçmeniz gerekmektedir.";
                    var rolList = cont.Rol.ToList().Select(k => new SelectListItem
                    {
                        Selected = true,
                        Text = k.Ad,
                        Value = k.Id.ToString()
                    }).ToList();
                    //Diyetisyen Rolunu selected olarak getiriyoruz.
                    var rolAsci = rolList.Where(l => l.Value == Convert.ToString(3)).FirstOrDefault();
                    rolAsci.Selected = true;
                    ViewBag.RolList = rolList;

                    var kullaniciList = cont.Kullanici.ToList().Select(k => new SelectListItem
                    {
                        Selected = true,
                        Text = k.Ad + " " + k.Soyad,
                        Value = k.Id.ToString()
                    }).ToList();
                    ViewBag.KullaniciList = kullaniciList;
                    return View(asci);

                }
                else
                {
                    asciEdit.KullaniciId = asci.KullaniciId;
                    cont.SaveChanges();
                    return RedirectToAction("ListAsci", "SistemKullanici", new { id = asciEdit.Id });
                }
            }
            return View();
        }


        //-----Asci silmek icin kullandigimiz action 
        public ActionResult DeleteAsci(int Id)
        {
            var asci = cont.Asci.Where(l => l.Id == Id).FirstOrDefault();
            return PartialView(asci);
        }

        [HttpPost, ActionName("DeleteAsci")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAsciOnay(Asci asci)
        {
            var asciSil = cont.Asci.Where(l => l.Id == asci.Id).FirstOrDefault();
            cont.Asci.Remove(asciSil);
            cont.SaveChanges();
            return RedirectToAction("ListAsci", "SistemKullanici");
        }



        //Sistemde yer alan Kuryelerin listelendiği action
        public ActionResult ListKurye()
        {
            var kuryeList = cont.Kurye.ToList();
            if (kuryeList.Count() > 0)
            {


                foreach (var item in kuryeList)
                {
                    item.AsciAdi = cont.Asci.Where(l=>l.Id==item.AsciId).Select(l => l.Kullanici.Ad + " " + l.Kullanici.Soyad).FirstOrDefault();
                }
               
            }
            return View(kuryeList);
        }

        //Sisteme yeni kurye eklemek için kullanığımız action method
        public ActionResult CreateKurye()
        {
            var rolList = cont.Rol.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad,
                Value = k.Id.ToString()
            }).ToList();
            //Kuryenin Rolunu selected olarak getiriyoruz.
            var rolKurye = rolList.Where(l => l.Value == Convert.ToString(5)).FirstOrDefault();
            rolKurye.Selected = true;
            ViewBag.RolList = rolList;

            var kullaniciList = cont.Kullanici.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad + " " + k.Soyad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.KullaniciList = kullaniciList;

            var asciList = cont.Asci.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Kullanici.Ad + " " + k.Kullanici.Soyad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.AsciList = asciList;
            return View();
        }
        [HttpPost]
        public ActionResult CreateKurye(Kurye kurye)
        {
            var rolId = cont.Kullanici.Where(l => l.Id == kurye.KullaniciId).Select(l => l.RolId).FirstOrDefault();
            if (rolId != 5)
            {
                ViewBag.Error = "Rolü kurye olan bir kullanıcı seçmeniz gerekmektedir.";
                var rolList = cont.Rol.ToList().Select(k => new SelectListItem
                {
                    Selected = false,
                    Text = k.Ad,
                    Value = k.Id.ToString()
                }).ToList();
                //Kuryenin Rolunu selected olarak getiriyoruz.
                var rolKurye = rolList.Where(l => l.Value == Convert.ToString(5)).FirstOrDefault();
                rolKurye.Selected = true;
                ViewBag.RolList = rolList;

                var kullaniciList = cont.Kullanici.ToList().Select(k => new SelectListItem
                {
                    Selected = false,
                    Text = k.Ad + " " + k.Soyad,
                    Value = k.Id.ToString()
                }).ToList();
                ViewBag.KullaniciList = kullaniciList;

                var asciList = cont.Kullanici.Where(l => l.RolId == 4).ToList().Select(k => new SelectListItem
                {
                    Selected = false,
                    Text = k.Ad + " " + k.Soyad,
                    Value = k.Id.ToString()
                }).ToList();
                ViewBag.AsciList = asciList;
                return View(kurye);

            }
            else
            {
                kurye.AsciAdi = cont.Asci.Where(l => l.Id == kurye.AsciId).Select(l => l.Kullanici.Ad + " " + l.Kullanici.Soyad).FirstOrDefault();
                cont.Kurye.Add(kurye);
                cont.SaveChanges();
                return RedirectToAction("ListKurye", "SistemKullanici");
            }

        }

        //Sistemde yer alan bir kuryenin duzenlendigi action
        public ActionResult EditKurye(int id)
        {
            var rolList = cont.Rol.ToList().Select(k => new SelectListItem
            {
                Selected = false,
                Text = k.Ad,
                Value = k.Id.ToString()
            }).ToList();
            //Diyetisyen Rolunu selected olarak getiriyoruz.
            var rolAsci = rolList.Where(l => l.Value == Convert.ToString((int)EnumRol.Kurye)).FirstOrDefault();
            rolAsci.Selected = true;
            ViewBag.RolList = rolList;

            var kullaniciList = cont.Kullanici.Where(l=>l.RolId==(int)EnumRol.Kurye).ToList().Select(k => new SelectListItem
            {
                Selected = true,
                Text = k.Ad + " " + k.Soyad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.KullaniciList = kullaniciList;

            var asciList = cont.Asci.ToList().Select(k => new SelectListItem
            {
                Selected = true,
                Text = k.Kullanici.Ad + " " + k.Kullanici.Soyad,
                Value = k.Id.ToString()
            }).ToList();
            ViewBag.AsciList = asciList;
            var kurye = cont.Kurye.Find(id);
            return View(kurye);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditKurye(Kurye kurye)
        {
            var kuryeEdit = cont.Kurye.Find(kurye.Id);
            if (kuryeEdit != null)
            {
                if (kuryeEdit.Kullanici.RolId != (int)EnumRol.Kurye)
                {
                    ViewBag.Error = "Rolü kurye olan bir kullanıcı seçmeniz gerekmektedir.";
                    var rolList = cont.Rol.ToList().Select(k => new SelectListItem
                    {
                        Selected = true,
                        Text = k.Ad,
                        Value = k.Id.ToString()
                    }).ToList();
                    //Kurye Rolunu selected olarak getiriyoruz.
                    var rolKurye = rolList.Where(l => l.Value == Convert.ToString((int)EnumRol.Kurye)).FirstOrDefault();
                    rolKurye.Selected = true;
                    ViewBag.RolList = rolList;

                    var kullaniciList = cont.Kullanici.ToList().Select(k => new SelectListItem
                    {
                        Selected = true,
                        Text = k.Ad + " " + k.Soyad,
                        Value = k.Id.ToString()
                    }).ToList();
                    ViewBag.KullaniciList = kullaniciList;
                    var asciList = cont.Asci.ToList().Select(k => new SelectListItem
                    {
                        Selected = true,
                        Text = k.Kullanici.Ad + " " + k.Kullanici.Soyad,
                        Value = k.Id.ToString()
                    }).ToList();
                    ViewBag.AsciList = asciList;
                    return View(kurye);

                }
                else
                {
                    kuryeEdit.AsciAdi = cont.Asci.Where(l => l.Id == kurye.AsciId).Select(l => l.Kullanici.Ad + " " + l.Kullanici.Soyad).FirstOrDefault();
                    kuryeEdit.KullaniciId = kurye.KullaniciId;
                    kuryeEdit.AsciId = kurye.AsciId;
                    cont.SaveChanges();
                    return RedirectToAction("ListKurye", "SistemKullanici", new { id = kuryeEdit.Id });
                }
            }
            return View();
        }

        //-----Kurye silmek icin kullandigimiz action 
        public ActionResult DeleteKurye(int Id)
        {
            var kurye = cont.Kurye.Where(l => l.Id == Id).FirstOrDefault();
            return PartialView(kurye);
        }

        [HttpPost, ActionName("DeleteKurye")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteKuryeOnay(Kurye kurye)
        {
            var kuryeSil = cont.Kurye.Where(l => l.Id == kurye.Id).FirstOrDefault();
            cont.Kurye.Remove(kuryeSil);
            cont.SaveChanges();
            return RedirectToAction("ListKurye", "SistemKullanici");
        }


        // GET: SistemKullanici
        public ActionResult Asci()
        {
            return View();
        }

        // GET: SistemKullanici
        public ActionResult Diyetisyen()
        {
            return View();
        }

        // GET: SistemKullanici
        public ActionResult Kurye()
        {
            return View();
        }
    }
}