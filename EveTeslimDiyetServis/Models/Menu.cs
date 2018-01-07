using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETDS.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        public string Ad { get; set; }

        public string Aciklama { get; set; }

        public int DiyetisyenId { get; set; }

        public int PaketId { get; set; }

        public int OgunId { get; set; }

        [DisplayFormat(DataFormatString = "{dd/mm/yyyy}")]
        public DateTime MenuTarih { get; set; }

        public virtual Paket Paket { get; set; }

        public virtual Ogun Ogun { get; set; }

        public virtual Diyetisyen Diyetisyen { get; set; }


    }
}