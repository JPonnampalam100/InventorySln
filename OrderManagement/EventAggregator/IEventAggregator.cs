using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.EventAggregator
{
    public interface IEventAggregator
    {
        event EventHandler<NewOrderEventArgs> DataPublisher;
        void Publish(NewOrderEventArgs args);
        void Subscribe(EventHandler<NewOrderEventArgs> eventHandler);
    }
}
