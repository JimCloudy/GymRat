using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkingOut.Startup))]
namespace WorkingOut
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
