using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventSite.Models
{
    public class Order
    {
        public int ID { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();

        public string CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual ApplicationUser User { get; set; }

        public bool IsFulfilled { get; set; } = false;

        public DateTime TimeCreated { get; set; }

        [NotMapped]
        public double TotalPrice
        {
            get 
            {
                return Tickets.Sum(s => s.Price);
            }            
        }
    }
}