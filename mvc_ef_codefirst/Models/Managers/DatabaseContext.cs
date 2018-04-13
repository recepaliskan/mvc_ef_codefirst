using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace mvc_ef_codefirst.Models.Managers
{
    public class DatabaseContext:DbContext//yetkili kılmak için DbContext yazdık ctrl+nokta ile ekledik
    {
        public DbSet<Kisiler> Kisiler { get; set; }
        public DbSet<Adresler> Adresler { get; set; }

        public DatabaseContext()//bunu en son yaptık
        {
            Database.SetInitializer(new VeritabaniOlusturucu());//databaseyi alıyor burdan aşağıda yok ise create database ıf not exists
        }

    }

    public class VeritabaniOlusturucu : CreateDatabaseIfNotExists<DatabaseContext> //database createdatabase... den miras alıyor burda database yok ise diyor,başka şeyleri de var
    {
        protected override void Seed(DatabaseContext context)
        {//kişiler insert ediliyor
            for (int i = 0; i <10; i++)
            {
                Kisiler kisi = new Kisiler();
                kisi.Ad = FakeData.NameData.GetFirstName();//fakedata indirdik nuget tan herhangi bir veri getiriyor mesela bi ad getiriyor şimdi
                kisi.Soyad = FakeData.NameData.GetSurname();
                kisi.Yas = FakeData.NumberData.GetNumber(10,90);//10 ile 90 arası bi yaş verecek fakedata

                context.Kisiler.Add(kisi); //context databasemiz ekletiyoruz Kişiler tamloma insert yani ekleme yapıyoruz

            }
            context.SaveChanges();//şimdi 10 kişi kaydetmiş olacak

            //adresler insert ediliyor
            List<Kisiler> tumKisiler = context.Kisiler.ToList();//Tüm kişiler gelecel yani select * from kişiler
            //foreach ile kişilerin hepsinde dönüyoruz
            foreach (Kisiler kisi in tumKisiler)
            {
                for (int i = 0; i < FakeData.NumberData.GetNumber(1,5); i++)//1 ile 5   arası kadar adres veriyor kişiler 
                {
                    Adresler adres = new Adresler();
                    adres.AdresTanim = FakeData.PlaceData.GetAddress();
                    adres.Kisi = kisi;//kişilere adres attık

                    context.Adresler.Add(adres);
                }
            }
            context.SaveChanges();
        }



    }
}