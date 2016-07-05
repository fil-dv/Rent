using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract.Identity
{
    public interface IUser
    {
        int Id { get; set; }
        string UserName { get; set; }
        string UserPassword { get; set; }
        string UserRole { get; set; }
        string UserEmail { get; set; }
    }
}
