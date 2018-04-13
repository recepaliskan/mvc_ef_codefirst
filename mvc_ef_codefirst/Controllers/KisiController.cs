using mvc_ef_codefirst.Models;
using mvc_ef_codefirst.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_ef_codefirst.Controllers
{
    public class KisiController : Controller
    {
        // GET: Kisi
        public ActionResult Yeni()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Yeni(Kisiler kisi)
        {
            DatabaseContext db = new DatabaseContext();
            db.Kisiler.Add(kisi);
            int sonuc = db.SaveChanges();//savechanges ın geri dönüş tipi int dir.

            if (sonuc > 0)
            {
                ViewBag.Result = "Kişi Kaydedildi";
                ViewBag.Status = "success";
            }
            else
            {
                ViewBag.Result = "Başarışız.Kişi Kaydedilemedi.";
                ViewBag.Status = "danger";
            }

            return View();
        }

        public ActionResult Duzenle(int? kisiid)//soru işareti null alabilen bir parametre yyaptık
        {
            Kisiler kisi = null;


            if (kisiid != null)
            {
                DatabaseContext db = new DatabaseContext();
                kisi = db.Kisiler.Where(x => x.ID == kisiid).FirstOrDefault();
            }


            return View(kisi);
        }
        [HttpPost]
        public ActionResult Duzenle(Kisiler model, int? kisiid)//soru işareti null alabilen bir parametre yyaptık
        {
            DatabaseContext db = new DatabaseContext();
            Kisiler kisi = db.Kisiler.Where(x => x.ID == kisiid).FirstOrDefault();

            if (kisi != null)
            {
                kisi.Ad = model.Ad;
                kisi.Soyad = model.Soyad;
                kisi.Yas = model.Yas;
                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = "Kişi Güncellendi";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Result = "Başarışız.Kişi Güncellenemedi.";
                    ViewBag.Status = "danger";
                }



            }



            return View();
        }

        [HttpGet]
        public ActionResult Sil(int? kisiid)
        {
            Kisiler kisi = null;


            if (kisiid != null)
            {

                DatabaseContext db = new DatabaseContext();

                kisi = db.Kisiler.Where(x => x.ID == kisiid).FirstOrDefault();
                


            }

            return View(kisi);


        }
        [HttpPost, ActionName("Sil")]// sil ile çalıştır 
        public ActionResult SilOk(int? kisiid)
        {



            if (kisiid != null)
            {

                DatabaseContext db = new DatabaseContext();

                Kisiler kisi = db.Kisiler.Where(x => x.ID == kisiid).FirstOrDefault();
                var kisiler = kisi.Adresler.ToList();
                db.Adresler.RemoveRange(kisiler);

                db.Kisiler.Remove(kisi);

                db.SaveChanges();

            }

            return RedirectToAction("homepage", "Home");
        }
    }
}