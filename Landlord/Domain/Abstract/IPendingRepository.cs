using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IPendingRepository
    {
        IEnumerable<Pending> Pendings { get; }
        void DeletePending(Pending pending);
        void AddOrUpdatePending(Pending pending);
        void SavePendingsChanges();
       
    }
}
