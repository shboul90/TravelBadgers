using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelBadgers.WebMVC.Startup))]
namespace TravelBadgers.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
