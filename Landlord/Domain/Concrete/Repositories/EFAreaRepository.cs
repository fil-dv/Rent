using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Data.Entity.Migrations;

namespace Domain.Concrete.Repositories
{
    public class EFAreaRepository : IAreaRepository

    {
        EFDbContext _context = new EFDbContext();

        public IEnumerable<Area> Areas
        {
            get
            {
                return _context.Areas;
            }
        }

        public void SaveArea(Area area)
        {
            _context.Areas.AddOrUpdate(area);
            _context.SaveChanges();
        }
    }
}
