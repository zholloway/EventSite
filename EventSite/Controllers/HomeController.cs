using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventSite.Models;

namespace EventSite.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var events = HttpRuntime.Cache["eventList"];

            if(events == null)
            {
                var eventList = db.Events.ToList();

                HttpRuntime.Cache
                .Add("eventList",
                eventList,
                null,
                DateTime.Now.AddDays(7),
                new TimeSpan(0, 0, 0),
                System.Web.Caching.CacheItemPriority.High,
                null
                );

                events = HttpRuntime.Cache["eventList"];
            }         

            return View(events);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}