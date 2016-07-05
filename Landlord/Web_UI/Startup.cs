using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web_UI.Startup))]
namespace Web_UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
