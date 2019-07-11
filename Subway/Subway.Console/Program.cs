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
            List<Daejeon> list = DaejeonData.GetAll();
            foreach (var item in list)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
