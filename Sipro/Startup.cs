using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sipro.Startup))]
namespace Sipro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
