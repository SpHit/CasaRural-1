using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CasaRural.Startup))]
namespace CasaRural
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
