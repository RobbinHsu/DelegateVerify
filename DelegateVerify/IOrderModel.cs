using System;

namespace DelegateVerify
{
    public interface IOrderModel
    {
        void Save(Order order, Action<Order> insertCallback, Action<Order> updateCallback);
        void Delete(Func<Order, bool> predicate);
    }
}