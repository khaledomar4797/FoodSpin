using FoodSpin.Services;
using FoodSpin.Services.Dashboard;
using FoodSpin.Services.User;
using FoodSpin.WebMVC.Controllers;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace FoodSpin.WebMVC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IDashboardService, DashboardService>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}