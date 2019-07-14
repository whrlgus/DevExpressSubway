using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subway.Data
{
    partial class Daejeon
    {
        public override string ToString()
        {
            return 날짜시간.ToString()+" "+역번호+" "+역명+" "+구분+" "+승객수.ToString();
        }
    }
}
