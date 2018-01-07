using EveTeslimDiyetServis.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ETDS.Models
{
    public class Siparis
    {
        [Key]
        public int Id { get; set; }

        public string Aciklama { get; set; }

        public int KullaniciId { get; set; }

        public int? AsciId { get; set; }

        public DateTime Tarih { get; set; }

        public int? MenuId { get; set; }

        public int? KuryeId { get; set; }

        public int DurumId { get; set; }

        public int PaketId { get; set; }

        public int OgunId { get; set; }

        public string Adres { get; set; }

        public virtual Asci Asci { get; set; }
        [ForeignKey("KullaniciId")]
        public virtual Kullanici Kullanici { get; set; }

        public virtual Kurye Kurye { get; set; }

        public virtual Menu Menu { get; set; }

        public virtual Paket Paket { get; set; }

        public virtual Ogun Ogun { get; set; }

        [ForeignKey("DurumId")]
        public virtual SiparisDurum SiparisDurum { get; set; }
    }
}