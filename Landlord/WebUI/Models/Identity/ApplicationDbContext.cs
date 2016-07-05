using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.Identity
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("EFDbContext")
        { }

        //Для создание при старте приложения
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<WebUI.Models.Identity.CustomUserLogin> CustomUserLogins { get; set; }
    }

}