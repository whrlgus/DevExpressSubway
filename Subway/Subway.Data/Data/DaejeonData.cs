using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Subway.Data
{
    public class DaejeonData
    {
        public List<Daejeon> GetAll(Expression<Func<Daejeon, bool>> predicate = null)
        {
            using (SubwayEntities context = DbContextFactory.Create())
            {
                IQueryable<Daejeon> query = context.Set<Daejeon>();

                if (predicate != null)
                    query = query.Where(predicate);

                var list = query.ToList();

                return list;
            }
        }
        public List<string> GetStationNames()
        {
            using (SubwayEntities context = DbContextFactory.Create())
            {
                IQueryable<Daejeon> query = from x in context.Daejeons select x;
                var q = query.Select(x => new { x.역번호, x.역명 }).Distinct().OrderBy(x => x.역번호);
                return q.Select(x => x.역명).ToList();
            }
        }

        public List<int?> GetPassengers(Expression<Func<Daejeon, bool>> predicate = null)
        {
            using (SubwayEntities context = DbContextFactory.Create())
            {
                IQueryable<Daejeon> query = from x in context.Daejeons select x;
                if (predicate != null)
                    query = query.Where(predicate);

                var r = query.GroupBy(x => new { x.요일, x.구분 }).OrderBy(x => x.Key).Select(x => x.Sum(y => y.승객수));

                return r.ToList();

            }
        }


    }
}
