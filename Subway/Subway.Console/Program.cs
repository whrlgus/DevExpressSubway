using Subway.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subway.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> stations = DataRepository.Daejeon.GetStationNames();
            List<Daejeon> list = DataRepository.Daejeon.GetAll();
            var l = DataRepository.Daejeon.GetPassengers();
            foreach (var item in l)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
