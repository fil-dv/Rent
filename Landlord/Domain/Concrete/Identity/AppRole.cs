using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete.Identity
{
    public class AppRole : IdentityRole<int, CustomUserRole>
    {
        public void CustomRole() { }
        public void CustomRole(string name) { Name = name; }
    }
}
