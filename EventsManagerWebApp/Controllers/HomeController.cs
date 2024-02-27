using EventsManagerWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EventOperation;
using EventEntities;
using System.Data.SqlClient;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Extensions.Logging;
using System;

namespace EventsManagerWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Events ets = new Events();
            return View(ets.GetEvents());
        }

        public IActionResult Edit(int id)
        {
            Events ets = new Events();
            Event ev = ets.GetEvents(id)[0];
            return View(ev);
        }

        public IActionResult Update(int EventID, string EventName, DateTime EventDate, string EventLocation, 
                                    string newEventName, DateTime newEventDate, string newEventLocation)
        {
            string errMsg = "";
            Events ets = new Events();
            bool isSucceed = ets.UpdateEvent(EventID, EventName, EventDate, EventLocation,
                                     newEventName, newEventDate, newEventLocation, out errMsg);
            if (isSucceed)
            {
                TempData["isUpdateSuccessful"] = true;
            }
            else
            {
                TempData["isUpdateSuccessful"] = false;
                TempData["Error"] = errMsg;
            }
            //return RedirectToAction("Index");
            return RedirectToAction("Edit", "Home", new { id = EventID });
        }

        public IActionResult Delete(int id)
        {
            Events ets = new Events();
            string errMsg = "";
            if(!ets.DeleteEvent(id, out errMsg))
            {
                TempData["Error"] = errMsg;
            }
            else
            {
                TempData["isDeleteSuccessful"] = true;
            }
            return RedirectToAction("Index");
        }

        //public IActionResult ManageParticepants(int evetnId)
        //{
        //    return RedirectToAction("Index", "Registrations", new { eventID = evetnId });
        //}

        public IActionResult AddANewEvent(string newEventName, DateTime newEventDate, string newEventLocation)
        {
            Events ets = new Events();
            bool isAdded = ets.AddANewEvent(newEventName, newEventDate, newEventLocation);
            if (!isAdded)
            {
                TempData["isAddFailed"] = true;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
