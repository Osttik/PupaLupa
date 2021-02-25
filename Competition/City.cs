using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Competition
{
    public class City
    {
        public int Score { get; set; }
        public Dictionary<string, CityStreet> Streets { get; set; } = new Dictionary<string, CityStreet>();
        public DataRead Data { get; set; }

        public void Simulate()
        {
            for (int i = 0; i < Data.SimulationTime; i++)
            {
                foreach (CityStreet street in Streets.Values)
                {
                    street.Move();

                    if(street.IsGreen)
                    {
                        var dequedCar = street.CarQueueStandby.Dequeue();
                        var nextStreet = Streets[dequedCar.StreetsNames.Dequeue()];
                        nextStreet.CarMoving.Add(new CarPairTime() { Car = dequedCar, TimeTillEnd = nextStreet.TimeToCross });
                    }
                }
            }
        }

        public void SetData(DataRead data)
        {
            Data = data;

            foreach (var item in data.Streets)
            {
                CityStreet street = new CityStreet()
                {
                    Name = item.Name,
                    StartIndex = item.StartIntersectionIndex,
                    EndIndext = item.EndIntersectionIndex,
                    TimeToCross = item.TimeToCross,
                    IsGreen = false,
                    Tiked = 0,
                    TimeGreen = 1,
                    TimeRed = 1,
                };

                Streets.Add(street.Name, street);
            }

            foreach (var item in data.Cars)
            {
                string name = item.StreetsNames.Dequeue();
                Streets[name].CarQueueStandby.Enqueue(item);
            }

            foreach (var streetFrom in Streets.Values)
            {
                foreach (var streetTo in Streets.Values)
                {
                    if (streetFrom.EndIndext == streetTo.StartIndex)
                    {
                        streetFrom.CitiesToVyihaty.Add(streetTo.Name, streetTo);
                    }
                }
            }
        }
    }
}