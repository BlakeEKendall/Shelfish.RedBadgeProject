using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shelfish.WebMVC.Startup))]
namespace Shelfish.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
