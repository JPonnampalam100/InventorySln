using Autofac;
using OrderManagement.Interfaces;
using System.Windows;

namespace OrderManagement
{
    public class ViewModelLocator
    {
        public IMainViewModel MainViewModel
        {
            get
            {
                var container = ((App)Application.Current).Container;
                return container.Resolve<IMainViewModel>();
            }
        }

        public IOrdersViewModel OrdersViewModel
        {
            get
            {
                var container = ((App)Application.Current).Container;
                return container.Resolve<IOrdersViewModel>();
            }
        }
    }
}
