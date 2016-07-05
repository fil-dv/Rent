using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract.Identity
{
    public interface IUserStore<TUser> : IDisposable
    {
        void CreateAsinc(TUser user);
        void DeleteAsinc();
        TUser FindByIdAsinc(int userId);
        TUser FindByNameAsinc(string userName);
        void UpdateAsinc();
    }
}
