using Domain.Concrete.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Concrete.Identity
{
    public class AppUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateBirthday { get; set; }
    }
   
    
    

    

    

 
}