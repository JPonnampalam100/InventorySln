using Enitites;
using System.Collections.Generic;

namespace Repository
{
    public interface IRepository
    {
        List<Product> GetProducts();
        bool PlaceOrders(List<Order> orders);
    }
}
