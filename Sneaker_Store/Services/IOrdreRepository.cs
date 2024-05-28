using Sneaker_Store.Model;
using System.Collections.Generic;

namespace Sneaker_Store.Services
{
    public interface IOrderRepository
    {
        void AddOrdre(Ordre order);
        Ordre GetById(int orderId); // Add this method to retrieve order by ID
        List<Ordre> GetOrdersByCustomerId(int customerId);
        List<Ordre> GetAllOrders();
    }
}