using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition
{
    public class City
    {
        public int Score { get; set; }
        public Dictionary<string, CityStreet> Streets { get; set; }
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
                    }
                }
            }
        }
    }
}
