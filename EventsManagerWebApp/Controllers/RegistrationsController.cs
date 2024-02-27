using EventEntities;
using EventOperation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventsManagerWebApp.Controllers
{
    public class RegistrationsController : Controller
    {
        public IActionResult Index(int eventID, string eventName)
        {
            EventRegistrations ers = new EventRegistrations();
            List<EventRegistration> eventRegistrations = ers.GetEventRegistrations(eventID);
            TempData["EventName"] = eventName;
            TempData["EventID"] = eventID;
            return View(eventRegistrations);
        }

        public IActionResult Edit(int RegistrationID, int EventID, string ParticipantName, string ParticipantEmail)
        {
            EventRegistrations ers = new EventRegistrations();
            ers.UpdateARegistration(RegistrationID, ParticipantName, ParticipantEmail);
            TempData["isUpdateSuccessful"] = true;
            return RedirectToAction("Index", new { EventID });
        }

        public IActionResult Delete(int registrationID, int eventID, string eventName)
        {
            EventRegistrations ers = new EventRegistrations();
            ers.DeleteARegistration(registrationID);
            return RedirectToAction("Index", new { eventID = eventID, eventName = eventName });
        }

        public IActionResult AddANewParticipant(int eventID, string newParticipantName,
                                string newParticipantEmail)
        {
            EventRegistrations ers = new EventRegistrations();
            if (!ers.AddANewParticipant(eventID, newParticipantName, newParticipantEmail))
            {
                TempData["isAddFailed"] = true;
            }
            return RedirectToAction("Index", new { eventID });
        }
    }
}
