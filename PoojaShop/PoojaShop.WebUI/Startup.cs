using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PoojaShop.WebUI.Startup))]
namespace PoojaShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
