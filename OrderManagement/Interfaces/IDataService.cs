using OrderManagement.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OrderManagement.Interfaces
{
    public interface IDataService
    {
        Task<ObservableCollection<ProductModel>> GetProducts();
        Task<Tuple<bool, string>> PlaceOrder(ObservableCollection<OrdersModel> orders);
    }
  
}
