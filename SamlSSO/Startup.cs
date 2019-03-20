using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SamlSSO.Startup))]
namespace SamlSSO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
