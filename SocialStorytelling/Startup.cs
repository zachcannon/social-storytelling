using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialStorytelling.Startup))]
namespace SocialStorytelling
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
