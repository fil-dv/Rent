using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using WebUI.Models.Identity;

[assembly: OwinStartupAttribute(typeof(Web_UI.Startup))]
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
