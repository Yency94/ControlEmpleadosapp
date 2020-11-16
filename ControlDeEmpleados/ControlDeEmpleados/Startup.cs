using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ControlDeEmpleados.Startup))]
namespace ControlDeEmpleados
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
