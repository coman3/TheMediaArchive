using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Coman3.Startup))]
namespace Coman3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}