using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TallamondInventory.Startup))]
namespace TallamondInventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
