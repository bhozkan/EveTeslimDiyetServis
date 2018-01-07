using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ETDS.Models
{
    public class Diyetisyen
    {
        public int Id { get; set; }

        public int KullaniciId { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }
}