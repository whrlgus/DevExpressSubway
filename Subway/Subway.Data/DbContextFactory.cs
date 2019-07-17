using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subway.Data
{
    public partial class SubwayEntities
    {
        public SubwayEntities(string connectionString) : base(connectionString)
        {
        }
    }
    public class DbContextFactory
    {

        public static SubwayEntities Create()
        {
            //string connectionString = "metadata=res://*/Subway.csdl|res://*/Subway.ssdl|res://*/Subway.msl;provider=System.Data.SqlClient;provider connection string=\";data source=.;initial catalog=Subway;persist security info=True;user id=sa;password=a1234a;MultipleActiveResultSets=True;App=EntityFramework\";";
            string connectionString = "metadata=res://*/Subway.csdl|res://*/Subway.ssdl|res://*/Subway.msl;provider=System.Data.SqlClient;provider connection string=\";data source=10.10.14.99;initial catalog=Subway;persist security info=True;user id=sa;password=3512;MultipleActiveResultSets=True;App=EntityFramework\";";

            SubwayEntities context = new SubwayEntities(connectionString);
            //context.Database.Log = x => Debug.WriteLine(x);

            return context;
        }
    }
}
