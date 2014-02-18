using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Social_Storytelling.Startup))]
namespace Social_Storytelling
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
