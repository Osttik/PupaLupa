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
        public List<CityStreet> Streets { get; set; } = new List<CityStreet>();
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
                }
            }
        }
    }
}

TimeToCross