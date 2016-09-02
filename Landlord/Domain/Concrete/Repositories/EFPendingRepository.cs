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
    public class EFPendingRepository : IPendingRepository
    {
        EFDbContext _context = new EFDbContext();

        public IEnumerable<Pending> Pendings
        {
            get
            {
                return _context.Pendings;
            }
        }

        public void DeletePending(Pending pending)
        {
            _context.Pendings.Remove(pending);
        }

        public void AddOrUpdatePending(Pending pending)
        {
            _context.Pendings.AddOrUpdate(pending);
            _context.SaveChanges();
        }

        public void SavePendingsChanges()
        {
            _context.SaveChanges();
        }
    }
}
