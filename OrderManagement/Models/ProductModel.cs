using Newtonsoft.Json;
using System.ComponentModel;

namespace OrderManagement.Models
{
    public class ProductModel: INotifyPropertyChanged
    {
        private bool isSelected;
        private int quantity;
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; RaisePropertyChanged("Quantity"); }
        }
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
                if (IsSelected)
                {
                    RaisePropertyChanged("ProductsSelected");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
