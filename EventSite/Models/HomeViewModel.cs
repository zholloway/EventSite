using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventSite.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public Order ShoppingCart { get; set; }
    }
}