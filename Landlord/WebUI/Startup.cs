using Domain.Concrete.Identity;
using Domain.Concrete.Repositories.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
//using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
//using Owin;
using System;


namespace Web_UI
{
    public class Startup
    {
        public static Func<UserManager<AppUser, int>> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")
            });
            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<AppUser, int>(
                    new CustomUserStore(new ApplicationDbContext()));
                
                // добавляем при необходимости валидацию!!!
                usermanager.UserValidator = new UserValidator<AppUser, int>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };

                return usermanager;
            };
        }
    }
}
