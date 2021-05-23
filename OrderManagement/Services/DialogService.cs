using OrderManagement.Interfaces;
using OrderManagement.Models;
using OrderManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OrderManagement.Services
{
    public class DialogService : IDialogService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Place Order", MessageBoxButton.OK);
        }

        public void ShowOrderScreen(ObservableCollection<ProductModel> orderItems)
        {
            OrdersWindow window = new OrdersWindow();
            var dataContext = (OrdersViewModel)window.DataContext;
            dataContext.ProductsSelected = orderItems.ToList();
            window.Show();
        }

    }
}
