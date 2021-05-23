using Enitites;
using Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace InventoryService
{

    public class InventoryService : IInventoryService
    {
        private static readonly int DefaultWaitTime = 1500;
        private IRepository repository;
        public InventoryService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<List<Product>> GetProducts()
        {
            await SimulateDelay();
            try
            {
                return repository.GetProducts();
            }
            catch(KeyNotFoundException ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.NotFound);
            }
            catch(Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        public async Task<bool> PlaceOrder(List<OrderContract> orders)
        {
            await SimulateDelay();
            try
            {
                return repository.PlaceOrders(orders.ConvertAll(x=> new Order { ProductId=x.ProductId, Quantity=x.Quantity }));
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        private async Task SimulateDelay()
        {
            int timetoWait = ConfigurationManager.AppSettings["SimulateDelay"] == null ? DefaultWaitTime : Convert.ToInt32(ConfigurationManager.AppSettings["SimulateDelay"]);
            await Task.Delay(timetoWait);
        }
    }
}
