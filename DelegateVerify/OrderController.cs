using System;

namespace DelegateVerify
{
    public class OrderController
    {
        private readonly ILog _consoleLog;
        private readonly IOrderModel _orderModel;
        private string _message;

        public OrderController(IOrderModel orderModel)
        {
            _orderModel = orderModel;
            _consoleLog = new ConsoleLog();
        }

        public OrderController(IOrderModel orderModel, ILog consoleLog)
        {
            _orderModel = orderModel;
            _consoleLog = consoleLog;
        }

        public void Save(Order order)
        {
            _orderModel.Save(order, InsertMessage, UpdateMessage);
        }

        private void UpdateMessage(Order order)
        {
            _message = $"update order id:{order.Id} with {order.Amount} successfully!";
            _consoleLog.Write(_message);
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