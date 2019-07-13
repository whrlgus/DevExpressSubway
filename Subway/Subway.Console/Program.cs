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
            List<string> stations = DataRepository.Daejeon.GetStation();
            List<Daejeon> list = DataRepository.Daejeon.GetAll();
            
            foreach (var item in stations)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
