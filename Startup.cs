using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mis4200_Project.Startup))]
namespace mis4200_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
