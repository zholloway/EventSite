using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventSite.Models
{
    public class Ticket
    {
        public int ID { get; set; }

        public double Price { get; set; } = 29.50;

        public int EventID { get; set; }

        [ForeignKey("EventID")]
        public Event Event { get; set; }

        [NotMapped]
        public Guid TrackerId { get; set; } = Guid.NewGuid();

        public ICollection<Order> Orders { get; set; }
    }
}