using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mvc_ef_codefirst.Models
{
    [Table("Kisiler")]//tablomuzun ismini veriyoruz ctrl+nokta bas
    public class Kisiler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]//ID nın anahtar olduğunu belirttik ctrl+nokta bas ve ıdentity yaptık
        public int ID { get; set; }


        [StringLength(20),Required]//Uzunluğunu 20 karakter ile sınırlandırdık..Required boş gecilemez demek
        public string Ad { get; set; }
        
        [StringLength(20),Required]
        public string Soyad { get; set; }
        
        [Required]
        public int Yas { get; set; }

        public virtual List<Adresler> Adresler { get; set; }//bir kişinin birden çok adresi olabilir ondan ilişkilendirdik
    }
}