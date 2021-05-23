using OrderManagement.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OrderManagement.Interfaces
{
    public interface IDialogService
    {
        void ShowMessage(string message);
        void ShowOrderScreen(ObservableCollection<ProductModel> orderItems);
    }
}
