using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.EventAggregator
{
 
    /// <summary>
    /// Crude Event Aggregator, should use Prism ec
    /// </summary>
    public class EventAggregator: IEventAggregator
    {
        public event EventHandler<NewOrderEventArgs> DataPublisher;


        public void Publish(NewOrderEventArgs args)
        {
            if (DataPublisher != null)
            {
                DataPublisher(this, args);
            }
        }

        public void Subscribe(EventHandler<NewOrderEventArgs> eventHandler)
        {
            DataPublisher += eventHandler;
        }
    }
}
