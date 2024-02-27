using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEntities
{
    // Entity class for EventRegistrations table
    public class EventRegistration
    {
        public int RegistrationID { get; set; }
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string ParticipantName { get; set; }
        public string ParticipantEmail { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
