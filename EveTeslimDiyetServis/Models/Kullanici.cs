using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ETDS.Models
{
    public class Kullanici
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [Required]
        public string KullaniciAd { get; set; }
        public string EPosta { get; set; }
        public string Sifre { get; set; }
        public string Adres { get; set; }
        public string Gsm { get; set; }
        public double? Boy { get; set; }
        public double? Kilo { get; set; }
        public string Cinsiyet { get; set; }
        public string ProfilResmi { get; set; }
        public int? IlId { get; set; }
        [Required]
        public int RolId { get; set; }

        public virtual Rol Rol {get; set;}

        public virtual Il Il { get; set; }
    }
}