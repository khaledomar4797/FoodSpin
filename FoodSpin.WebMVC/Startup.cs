using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodSpin.WebMVC.Startup))]
namespace FoodSpin.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
