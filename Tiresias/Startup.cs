using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tiresias.Startup))]
namespace Tiresias
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
