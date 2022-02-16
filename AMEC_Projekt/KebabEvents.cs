using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulation
{
    public class KebabEvents
    {
        int numberInRessource = 0;
        int numberInQueue = 0;
        double ArrivalInterval = 10;
        double ServingTime = 5;

        int maxServings = 15;

        public int TotalServings { get; }

        public List<Event> FinishedEvents { get; set; }
        public List<Event> Events { get; set; }
        public int NQ { get; set; }
        public int NR { get; set; }

        public DateTime StartTime { get; set; }


        public KebabEvents(double arrivalInterval = 5, double servingTime = 7)
        {
            ArrivalInterval = arrivalInterval;
            ServingTime = servingTime;
            Events = new();
            FinishedEvents = new();
            TotalServings = 0;
            NQ = 0;
            NR = 0;
        }

        public void FillList()
        {
            var ga = new Event(EventType.GuestArrives);
            ga.TimeStamp = StartTime;
            Events.Add(ga);

            while (Events.Count < maxServings)
            {
                ga.TimeStamp = Events.Where(x => x.EventType == EventType.GuestArrives).OrderByDescending(x => x.TimeStamp).Last().TimeStamp;
                ga.TimeStamp.AddSeconds(ArrivalInterval);
                Events.Add(ga);
            }
        }

        public void ProcessList()
        {
            while (Events.Count > 0)
            {
                var currentCustomer = Events.Where(x => x.EventType == EventType.GuestArrives).OrderBy(x => x.TimeStamp).Last();
                Events.Remove(currentCustomer);
                var gl = new Event(EventType.GuestLeaves);
                if (FinishedEvents.Where(x => x.EventType == EventType.GuestLeaves).Count() > 0)
                    gl.TimeStamp = FinishedEvents.Where(x => x.EventType == EventType.GuestLeaves).OrderByDescending(x => x.TimeStamp).Last().TimeStamp;
                else
                    gl.TimeStamp = StartTime;
                gl.TimeStamp.AddSeconds(ServingTime);
                FinishedEvents.Add(gl);
                FinishedEvents.Add(currentCustomer);
                Console.WriteLine(Events.Count);
            }
        }

        public class Event
        {
            public DateTime TimeStamp { get; set; }

            public EventType EventType { get; set; }

            public Event(EventType eventType)
            {
                TimeStamp = DateTime.Now;
                EventType = eventType;
            }
        }

        public enum EventType
        {
            GuestLeaves,
            GuestArrives
        }
    }
}

