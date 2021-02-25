using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition
{
    public class DataRead
    {
        public int SimulationTime { get; set; }
        public int NumberOfIntersections { get; set; }
        public int NumberOfStreets { get; set; }
        public int NumberOfCars { get; set; }
        public int Bonus { get; set; }

        public List<Street> Streets { get; set; }
        public List<Car> Cars { get; set; }

        public DataRead()
        {
            Streets = new List<Street>();
            Cars = new List<Car>();
        }

        public static DataRead Read(string path)
        {
            DataRead data = new DataRead();

            string[] lines = File.ReadAllLines(path);

            string[] firstLine = lines[0].Trim().Split(' ');
            for(int i = 0; i < firstLine.Length; i++)
            {
                data.SimulationTime = Convert.ToInt32(firstLine[0]);
                data.NumberOfIntersections = Convert.ToInt32(firstLine[1]);
                data.NumberOfStreets = Convert.ToInt32(firstLine[2]);
                data.NumberOfCars = Convert.ToInt32(firstLine[3]);
                data.Bonus = Convert.ToInt32(firstLine[4]);
            }

            
            for (int i = 1; i <= data.NumberOfStreets; i++)
            {
                string[] values = lines[i].Trim().Split(' ');
                Street street = new Street()
                {
                    Name = values[2],
                    StartIntersectionIndex = Convert.ToInt32(values[0]),
                    EndIntersectionIndex = Convert.ToInt32(values[1]),
                    TimeToCross = Convert.ToInt32(values[3])
                };

                data.Streets.Add(street);
            }

            for (int i = data.NumberOfStreets + 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Trim().Split(' ');
                Car car = new Car
                {
                    StreetsNames = new List<string>(values.ToList().GetRange(1, values.Length - 1))
                };

                data.Cars.Add(car);
            }

            return data;
        }
    }

    public class Street
    {
        public string Name { get; set; }
        public int TimeToCross { get; set; }
        public int StartIntersectionIndex { get; set; }
        public int EndIntersectionIndex { get; set; }
    }

    public class Car
    {
        public List<string> StreetsNames { get; set; } 

        public Car()
        {
            StreetsNames = new List<string>();
        }
    }
}
