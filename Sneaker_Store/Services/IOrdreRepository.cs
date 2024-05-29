using Sneaker_Store.Model;
using System.Collections.Generic;

namespace Sneaker_Store.Services
{
    public interface IOrderRepository
    {
        void AddOrdre(Ordre order);
        Ordre GetById(int orderId);
        List<Ordre> GetOrdersByCustomerId(int customerId);
        List<Ordre> GetAllOrders();
        int CreateOrder(int kundeId);
        void AddSkoToOrder(int orderId, int skoId);
        List<Sko> GetSkoInOrder(int orderId);
    }
}