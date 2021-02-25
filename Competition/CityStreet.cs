using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition
{
    public class CityStreet
    {
        public string Name { get; set; }
        public int StartIndex { get; set; }
        public int EndIndext { get; set; }
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

            foreach (var pair in CarMoving)
            {
                pair.TimeTillEnd -= 1;
            }


        }
    }

    public class CarPairTime
    {
        public Car Car { get; set; }
        public int TimeTillEnd { get; set; }
    }
}
