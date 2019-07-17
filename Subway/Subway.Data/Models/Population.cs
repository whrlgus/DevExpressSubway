using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subway.Data
{
    public class Population
    {
        public DateTime Date { get; set; }

        public string StationNo { get; set; }

        public string StationName { get; set; }

        public int At5 { get; set; }

        public int At6 { get; set; }
    }
}
