using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AskAway.Startup))]
namespace AskAway
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
