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
        public List<CityStreet> Streets { get; set; }
        public DataRead Data { get; set; }

        public void Simulate()
        {
            for (int i = 0; i < Data.SimulationTime; i++)
            {
                foreach (CityStreet street in Streets)
                {
                    street.Move();

                    if(street.IsGreen)
                    {

                    }
                }
            }
        }
    }
}
