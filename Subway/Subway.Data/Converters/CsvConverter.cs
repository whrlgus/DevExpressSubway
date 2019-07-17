using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subway.Data
{
    public abstract class CsvConverter<T>
    {
        public List<T> Convert(string csvFilePath)
        {
            string[] lines = File.ReadAllLines(csvFilePath);

            List<T> list = new List<T>(lines.Length);

            for (int i = 1; i < lines.Length; i++)
            {
                // tokenize
                string[] tokens = lines[i].Split(',');

                T item = ParseItem(lines[i]);

                list.Add(item);
            }

            return list;
        }

        protected abstract T ParseItem(string line);
    }
}
