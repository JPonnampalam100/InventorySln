using Autofac;
using Repository;

namespace InventoryService
{
    public class RegisterIoc
    {
        public static IContainer Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<InventoryService>().As<IInventoryService>();
            builder.RegisterType<Repository.Repository>().As<IRepository>().SingleInstance();
            var container = builder.Build();
            return container;
        }
    }
}