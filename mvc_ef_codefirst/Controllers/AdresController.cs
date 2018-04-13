using mvc_ef_codefirst.Models;
using mvc_ef_codefirst.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_ef_codefirst.Controllers
{
    public class AdresController : Controller
    {
        // GET: Adres
        public ActionResult Yeni()
        {
            DatabaseContext db = new DatabaseContext();
            //List<Kisiler> kisiler = db.Kisiler.ToList();

            //List<SelectListItem> kisilerList = new List<SelectListItem>();
            //foreach (Kisiler kisi in kisiler)
            //{
            //    SelectListItem item = new SelectListItem();
            //    item.Text = kisi.Ad + " " + kisi.Soyad;//ekrana bu gösteriliyor
            //    item.Value = kisi.ID.ToString();//arkada ID sini saklıyor
            //    kisilerList.Add(item);

            //}

            //ViewBag.kisiler = kisilerList; //linq suz


            List<SelectListItem> kisilerList = (from kisi in db.Kisiler.ToList()
                                                select new SelectListItem()
                                                {
                                                    Text = kisi.Ad + " " + kisi.Soyad,
                                                    Value = kisi.ID.ToString()
                                                }).ToList();// LINQ lu


            TempData["kisiler"] = kisilerList;

            ViewBag.kisiler = kisilerList;

            return View();
        }


        [HttpPost]
        public ActionResult Yeni(Adresler adres)
        {

            DatabaseContext db=new DatabaseContext();
            Kisiler kisi = db.Kisiler.Where(x => x.ID == adres.Kisi.ID).FirstOrDefault();


            if (kisi != null)
            {

                adres.Kisi = kisi;

                db.Adresler.Add(adres);
                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = "Adres Kaydedildi";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Result = "Başarışız.Adres Kaydedilemedi.";
                    ViewBag.Status = "danger";
                }
            }

            ViewBag.kisiler = TempData["kisiler"];

            return View();


        }

        public ActionResult Duzenle(int? adresid)
        {
            Adresler adres = null;
            if (adresid != null) {
                DatabaseContext db = new DatabaseContext();

                List<SelectListItem> kisilerlist = (from kisi in db.Kisiler.ToList()
                                                    select new SelectListItem()
                                                    {
                                                        Text = (kisi.Ad + " " + kisi.Soyad).ToString(),
                                                        Value = kisi.ID.ToString()
                                                    }).ToList();

                TempData["kisiler"] = kisilerlist;
                ViewBag.kisiler = kisilerlist;
                adres = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();
                
            }
            

            return View(adres);
        }
        [HttpPost]
        public ActionResult Duzenle(Adresler model, int? adresid)
        {

            DatabaseContext db = new DatabaseContext();
            Kisiler kisi = db.Kisiler.Where(x => x.ID == model.Kisi.ID).FirstOrDefault();
            Adresler adres = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();


            if (kisi != null)
            {

                adres.Kisi = kisi;
                adres.AdresTanim = model.AdresTanim;


                
                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = "Adres güncellendi.";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Result = "Başarışız.Adres Güncellenemedi.";
                    ViewBag.Status = "danger";
                }
            }

            ViewBag.kisiler = TempData["kisiler"];

            return View();


        }

        public ActionResult Sil(int? adresid)
        {
            Adresler adres = null;


            if(adresid != null)
            {

                DatabaseContext db = new DatabaseContext();

                adres = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();


            }

            return View(adres);


        }
        [HttpPost, ActionName("Sil")]// sil ile çalıştır 
        public ActionResult SilOk(int? adresid)
        {



            if (adresid != null)
            {

                DatabaseContext db = new DatabaseContext();

                Adresler adres = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();

                db.Adresler.Remove(adres);

               

                db.SaveChanges();

            }

            return RedirectToAction("homepage", "Home");
        }
    
    }
    
 
}