using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Models
{
    public class OrdersModel :  INotifyPropertyChanged
    {
        private int availableQuantity;
        private bool isSelected;
        [JsonIgnore]
        public int AvailableQuantity
        {
            get { return availableQuantity; }
            set { availableQuantity = value; RaisePropertyChanged("AvailableQuantity"); }
        }

        [JsonIgnore]
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; RaisePropertyChanged("IsSelected"); }
        }

        private int quantity;
        public int ProductId { get; set; }
        [JsonIgnore]
        public string Name { get; set; }
        
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; RaisePropertyChanged("Quantity"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
