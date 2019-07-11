using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Subway.Data
{
    public class DaejeonData
    {
        static DaejeonData() { }
        public static List<Daejeon> GetAll(Expression<Func<Daejeon, bool>> predicate = null)
        {
            using (DbContext context = new SubwayEntities())
            {
                IQueryable<Daejeon> query = context.Set<Daejeon>();

                if (predicate != null)
                    query = query.Where(predicate);

                var list = query.ToList();

                return list;
            }
        }

    }
}
