using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Myndie.Models
{
    public class SellItem
    {
        public int Id { get; set; }
        [Required]
        public double PriceItem { get; set; }
        [Required]
        public int ApplicationId { get; set; }
        [Required]
        public int SellId { get; set; }
        public virtual Application Applications { get; set; }
        public virtual Sell Sells { get; set; }
    }
}