//using Domain.Abstract.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract.Identity
{
    public interface IUserClaimStore<TUser> : IUserStore<TUser>, IDisposable
    {
        void AddClaimAsinc(string claim);
        IEnumerable<string> GetClaimsAsinc(); // !!! может, другой возвращаемый тип, string???
        void RemoveClaimAsinc(string claim);
    }
}
