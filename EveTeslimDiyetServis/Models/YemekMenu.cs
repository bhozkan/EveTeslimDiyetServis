using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ETDS.Models;

namespace ETDS.Models
{
    public class YemekMenu
    {
        [Key]
        public int Id { get; set; }

        public int MenuId { get; set; }
        
        public int YemekId { get; set; }
        
        public virtual Menu Menu { get; set; }

        public virtual Yemek Yemek { get; set; }
    }
}