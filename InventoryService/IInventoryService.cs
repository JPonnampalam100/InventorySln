using Enitites;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace InventoryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IInventoryService
    {

        [WebInvoke(Method = "GET", UriTemplate = "/GetProducts", ResponseFormat = WebMessageFormat.Json)]
        Task<List<Product>> GetProducts();

        [OperationContract]
        [WebInvoke(UriTemplate = "/PlaceOrder",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        Task<bool> PlaceOrder(List<OrderContract> orders);
    }

    [DataContract]
    public class OrderContract
    {
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int Quantity { get; set; }

    }

}
