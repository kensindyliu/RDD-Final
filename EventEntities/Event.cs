using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEntities
{
    // Entity class for Events table
    public class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public Event() { }
        public Event(int eventID, string eventName, DateTime eventDate, string EventLocation) 
        {
            this.EventID = eventID;
            this.EventName = eventName;
            this.EventDate = eventDate;
            this.EventLocation = EventLocation;
        }

    }
}
