using OrderManagement.EventAggregator;
using OrderManagement.Interfaces;
using System.ComponentModel;

namespace OrderManagement.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected IDataService DataService { get; private set; }
        protected IDialogService DialogService { get; private set; }
        protected IEventAggregator EventAggregator { get; private set; }
        public ViewModelBase(IDataService dataService, IDialogService dialogService, IEventAggregator eventAggregator)
        {
            DataService = dataService;
            DialogService = dialogService;
            EventAggregator = eventAggregator;
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
