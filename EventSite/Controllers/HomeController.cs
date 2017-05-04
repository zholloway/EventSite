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
            var events = HttpRuntime.Cache["eventList"] as IEnumerable<Event>;

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

                events = HttpRuntime.Cache["eventList"] as IEnumerable<Event>;
            }

            var vm = new HomeViewModel {
                Events = events,
                ShoppingCart = Session["cart"] as Order ?? new Order()
            };

            return View(vm);
        }

        [Authorize(Roles = "admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult ShoppingCart(int id)
        {
            var cart = Session["cart"] as Order;
            if (cart == null)
            {
                // create a new cart
                cart = new Order()
                {
                    IsFulfilled = false,
                    TimeCreated = DateTime.Now
                };
            }
            // to get the item
            var itemToAdd = new ApplicationDbContext().Tickets.FirstOrDefault(f => f.ID == id);
            // add item select to shopping cart
            cart.Tickets.Add(itemToAdd);
            Session["cart"] = cart;
            return PartialView("_shoppingCart", cart);
        }


        [HttpDelete]
        public ActionResult RemoveFromCart(string id)
        {

            var cart = Session["cart"] as Order;
            cart.Tickets = cart.Tickets.Where(w => w.TrackerId != Guid.Parse(id)).ToList();
            return PartialView("_checkoutDisplayCart", cart);
        }


        public ActionResult Checkout()
        {
            // Shopping Cart (order) as the model
            var vm = Session["cart"] as Order;
            return View(vm);
        }
    }
}