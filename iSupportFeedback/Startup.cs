using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iSupportFeedback.Startup))]
namespace iSupportFeedback
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
