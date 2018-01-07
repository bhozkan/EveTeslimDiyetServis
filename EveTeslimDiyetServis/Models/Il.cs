using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETDS.Models
{
    public class Il
    {
        [Key]
        public int Id { get; set; }

        public string Ad { get; set; }

    }
}