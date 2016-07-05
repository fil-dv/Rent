using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract.Identity
{    
    public interface IUserPasswordStore<TUser> : IUserStore<TUser>, IDisposable
    {
        string GetPasswordHashAsinc(string login);
        bool HasPasswordAsinc(string password);
        void SetPasswordHashAsinc(string password);
    }
}
