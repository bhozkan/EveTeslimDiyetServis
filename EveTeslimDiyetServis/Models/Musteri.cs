using ETDS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETDS.Models
{
    public class Musteri
    {
        [Key]
        public int Id { get; set; }
        public int KullaniciId { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }
}