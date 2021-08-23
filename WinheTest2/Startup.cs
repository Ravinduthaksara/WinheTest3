using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WinheTest2.Startup))]
namespace WinheTest2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
