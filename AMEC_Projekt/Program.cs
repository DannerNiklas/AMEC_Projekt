using System;
using System.Collections.Generic;
using static Kebab_Simulation.KebabEvents;

namespace Kebab_Simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();

            void Start()
            {
                KebabEvents kebabEvents = new KebabEvents();
                kebabEvents.StartTime = DateTime.Now;
                kebabEvents.FillList();
                kebabEvents.ProcessList();

                //Statistik:
                var test = kebabEvents.CalculateAllocation();
                Console.WriteLine("Minimum in Queue: " + test[0]);
                Console.WriteLine("Maximum in Queue: " + test[1]);
                Console.WriteLine("Average in Queue: " + test[2]);
            }
        }
    }
}


