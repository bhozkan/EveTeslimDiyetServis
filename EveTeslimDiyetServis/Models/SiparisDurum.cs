using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EveTeslimDiyetServis.Models
{
    public class SiparisDurum
    {
        [Key]
        public int Id { get; set; }

        public string Durum { get; set; }
    }
}