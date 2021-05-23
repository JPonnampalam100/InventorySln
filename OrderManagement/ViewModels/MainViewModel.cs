using OrderManagement.Commands;
using OrderManagement.EventAggregator;
using OrderManagement.Interfaces;
using OrderManagement.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrderManagement.ViewModels
{
    public class MainViewModel : ViewModelBase,  IMainViewModel
    {
        private ObservableCollection<ProductModel> products;
        public ObservableCollection<ProductModel> Prods
        {
            get { return products; }
            set { products = value; RaisePropertyChanged(nameof(Prods)); }
        }
        public MainViewModel(IDataService dataService, IDialogService dialogService, IEventAggregator eventAggregator):base(dataService, dialogService, eventAggregator)
        {
            LoadData();
            CreateOrder =new RelayCommand<object>(Execute, CanExecute);
            EventAggregator.Subscribe(ReceivedOrderUpdate);
        }

        private void ReceivedOrderUpdate(object sender,NewOrderEventArgs args)
        {
            LoadData();
        }

        public void Execute(object parameter)
        {
            DialogService.ShowOrderScreen(new ObservableCollection<ProductModel>(Prods.Where(p => p.IsSelected)));
        }

        public bool CanExecute(object parameter)
        {
            return Prods == null ? false : Prods.Where(p => p.IsSelected).Any();
        }

        public ICommand CreateOrder { get; private set; }

        async private Task LoadData()
        {
            Prods = await DataService.GetProducts();
        }
      
    }
}
