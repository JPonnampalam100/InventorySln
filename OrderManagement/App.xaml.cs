using Autofac;
using OrderManagement.Interfaces;
using OrderManagement.Models;
using OrderManagement.Services;
using OrderManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OrderManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IContainer Container { get; private set; }
        private void OnStartup(object sender,StartupEventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DataService>().As<IDataService>();
            // Register View Models
            builder.RegisterType<MainViewModel>().As<IMainViewModel>();
            builder.RegisterType<OrdersViewModel>().As<IOrdersViewModel>();

            //Services
            builder.RegisterType<DialogService>().As<IDialogService>();
            //Event Aggregator
            builder.RegisterType<EventAggregator.EventAggregator>().As<EventAggregator.IEventAggregator>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var window = scope.Resolve<MainWindow>();
                window.Show();
            }
        }
    }
}
