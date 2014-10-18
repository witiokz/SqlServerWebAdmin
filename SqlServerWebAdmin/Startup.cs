using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SqlServerWebAdmin.Startup))]
namespace SqlServerWebAdmin
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
