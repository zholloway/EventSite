using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventSite.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int GenreID { get; set; }
        public Genre Genre { get; set; }
    }
}