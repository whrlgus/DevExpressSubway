using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subway.Data
{
    public class PopulationConveter : CsvConverter<Population>
    {
        #region singleton
        private PopulationConveter()
        {
        }

        private static PopulationConveter _instance;

        public static PopulationConveter Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PopulationConveter();

                return _instance;
            }
        }
        #endregion

        protected override Population ParseItem(string line)
        {
            string[] tokens = line.Split(',');

            Population population = new Population();
            //population.Date = DateTime.ParseExact(tokens[0]);
            population.Date = DateTime.Parse(tokens[0]);
            population.StationNo = tokens[1].Trim();
            population.StationName = tokens[2].Trim();
            population.At5 = int.Parse(tokens[6]);
            population.At6 = int.Parse(tokens[7]);

            return population;
        }

    }
}
