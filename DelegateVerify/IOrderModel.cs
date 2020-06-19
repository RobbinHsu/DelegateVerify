using System;

namespace DelegateVerify
{
    public interface IOrderModel
    {
        void Save(Order order, Action<Order> insertCallback, Action<Order> updateCallback);
        void Delete(Func<Order, bool> predicate);
        event Action<Order> OnCreated;
        event Action<Order> OnUpdated;
        void Save(Order order);
    }
}