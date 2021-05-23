using OrderManagement.Commands;
using OrderManagement.EventAggregator;
using OrderManagement.Interfaces;
using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrderManagement.ViewModels
{
    public class OrdersViewModel : ViewModelBase, IOrdersViewModel
    {
        public OrdersViewModel(IDataService dataService, IDialogService dialogService, IEventAggregator eventAggregator) : base(dataService,dialogService, eventAggregator )
        {
            orders = new ObservableCollection<OrdersModel>();
            CreateOrder = new RelayCommand<object>(ExecuteOrder, CanExecuteOrder);
            DeleteOrder = new RelayCommand<object>(ExecuteDelete, CanExecuteDelete);
        }

        private ObservableCollection<OrdersModel> orders;
        private bool IsBusy { get; set; }
        public List<ProductModel> ProductsSelected
        {
            set 
            { 
                foreach(var product in value)
                {
                    OrdersModel order = new OrdersModel { AvailableQuantity = product.Quantity, Name=product.Name, ProductId = product.ProductId , IsSelected=false};
                    orders.Add(order);
                }
            }
        }

        public async void ExecuteOrder(object parameter)
        {
            try
            {
                IsBusy = true;
                var res = await DataService.PlaceOrder(Orders);
                DialogService.ShowMessage(res.Item1 == true ? "Order Placed" : $"Failed to Place Order {res.Item2}");
                ResetQuantities();
                EventAggregator.Publish(new NewOrderEventArgs());
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ResetQuantities()
        {
            foreach(var order in Orders)
            {
                order.IsSelected = false;
                order.Quantity = 0;
            }
        }

        public void ExecuteDelete(object parameter)
        {
            try
            {
                IsBusy = true;
                foreach (var order in Orders.Where(o => o.IsSelected).ToList())
                {
                    Orders.Remove(order);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
        public bool CanExecuteDelete(object parameter)
        {
            CommandManager.InvalidateRequerySuggested();
            return Orders.Any() && !IsBusy;
        }

        public bool CanExecuteOrder(object parameter)
        {
            CommandManager.InvalidateRequerySuggested();
            return Orders.Sum(p => p.Quantity) > 0 && !IsBusy;
        }

        public ObservableCollection<OrdersModel> Orders
        {
            get { return orders; }
            set { orders = value; RaisePropertyChanged(nameof(Orders)); }
        }

        public ICommand CreateOrder { get; private set; }
        public ICommand DeleteOrder { get; private set; }
    }
}
