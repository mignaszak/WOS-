using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WOS_.Startup))]
namespace WOS_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
