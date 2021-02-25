using System.Collections.Generic;
using static Competition.SimulationCounter;

namespace Competition
{
    public class CityStreet
    {
        public string Name { get; set; }
        public int StartIndex { get; set; }
        public int EndIndext { get; set; }
        public int TimeToCross { get; set; }
        public Dictionary<string, CityStreet> CitiesToVyihaty { get; set; } = new Dictionary<string, CityStreet>();
        public Queue<Car> CarQueueStandby { get; set; } = new Queue<Car>();
        public List<CarPairTime> CarMoving { get; set; } = new List<CarPairTime>();
        public int TimeGreen { get; set; }
        public int TimeRed { get; set; }

        public bool IsGreen { get; set; } 
        public int Tiked { get; set; }

        public void Move()
        {
            Tiked++;
            if (Tiked >= TimeGreen && IsGreen)
            {
                Tiked = 0;
                IsGreen = false;
            }
            if (Tiked >= TimeRed && !IsGreen)
            {
                Tiked = 0;
                IsGreen = true;
            }

            List<CarPairTime> newList = new List<CarPairTime>();
            foreach (var pair in CarMoving)
            {
                pair.TimeTillEnd -= 1;
                if (pair.TimeTillEnd <= 0)
                {
                    //
                    CityLightsAwaitTime.Add(Name, CityLightsAwaitTime[Name] + CarQueueStandby.Count);
                    //
                    
                    CarQueueStandby.Enqueue(pair.Car);
                }
                else
                {
                    newList.Add(pair);
                }
            }
            CarMoving = newList;
        }
    }

    public class CarPairTime
    {
        public Car Car { get; set; }
        public int TimeTillEnd { get; set; }
    }
}
