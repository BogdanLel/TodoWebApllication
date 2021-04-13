using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TodoWebApllication.Startup))]
namespace TodoWebApllication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
