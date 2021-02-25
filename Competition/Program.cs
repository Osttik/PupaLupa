using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition
{
    class Program
    {

        static void Main(string[] args)
        {
            var data = DataRead.Read("D:/Temp Files/a.txt");

            City city = new City();
            city.SetData(data);
        }
    }
}
