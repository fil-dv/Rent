using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract.Identity
{    
    public interface IUserLoginStore<TUser> : IUserStore<TUser>, IDisposable
    {
        void AddLoginAsinc(string login);
        TUser FindAsinc(string login);      
        IEnumerable<string> GetLoginsAsinc();
        void RemoveLoginAsinc(string login);
    }
}
