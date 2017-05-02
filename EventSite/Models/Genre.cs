using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventSite.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Event> Events { get; set; }
    }
}