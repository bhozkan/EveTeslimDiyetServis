using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ETDS.Models
{
    public class Kurye
    {
        [Key]
        public int Id { get; set; }

        public int KullaniciId { get; set; }

        public int AsciId { get; set; }

        public string AsciAdi { get; set; }

        public virtual Kullanici Kullanici { get; set; }

       
    }
}