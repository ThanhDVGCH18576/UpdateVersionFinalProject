using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CWFinal_1628.Startup))]
namespace CWFinal_1628
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
