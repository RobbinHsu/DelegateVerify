using System;

namespace DelegateVerify
{
    public class OrderController
    {
        private readonly IOrderModel _orderModel;

        public OrderController(IOrderModel orderModel)
        {
            _orderModel = orderModel;
        }

        public void Save(Order order)
        {
            _orderModel.Save(order, InsertMessage, UpdateMessage);
        }

        private void UpdateMessage(Order order)
        {
            Console.WriteLine($"update order id:{order.Id} with {order.Amount} successfully!");
        }

        private void InsertMessage(Order order)
        {
            Console.WriteLine($"insert order id:{order.Id} with {order.Amount} successfully!");
        }

        public void DeleteAdultOrders()
        {
            _orderModel.Delete(o => o.Customer.Age >= 18);
        }
    }
}