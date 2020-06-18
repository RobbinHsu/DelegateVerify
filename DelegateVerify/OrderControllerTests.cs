using System;
using System.Security.Cryptography.X509Certificates;
using NSubstitute;
using NUnit.Framework;

namespace DelegateVerify
{
    [TestFixture]
    public class OrderControllerTests
    {
        private ILog _log;
        private IOrderModel _model;
        private Order _order;
        private OrderController _orderController;

        [SetUp]
        public void Setup()
        {
            _model = Substitute.For<IOrderModel>();
            _log = Substitute.For<ILog>();
            _orderController = new OrderController(_model, _log);
        }

        [Test]
        public void exist_order_should_update()
        {
            CreateOrder();
            WhenUpdateMessage();
            ShouldReceivedMessageContains("update");
        }

        [Test]
        public void no_exist_order_should_insert()
        {
            CreateOrder();
            WhenNoMessageInsert();
            ShouldNotReceiveMessage();
        }

        [Test]
        public void test_delete_adult_orders()
        {
            CreateOrderWhenCustomerAgeMoreThen(30);
            var modelDeletePredicate = WhenDeleteAdultOrder();
            ShouldDeleteAdultOrder(modelDeletePredicate);
        }

        private void ShouldDeleteAdultOrder(Func<Order, bool> modelDeletePredicate)
        {
            Assert.IsTrue(modelDeletePredicate(_order));
        }

        private Func<Order, bool> WhenDeleteAdultOrder()
        {
            Func<Order, bool> modelDeletePredicate = null;
            _model.When(m => m.Delete(Arg.Any<Func<Order, bool>>()))
                .Do(m => modelDeletePredicate = m[0] as Func<Order, bool>);

            _orderController.DeleteAdultOrders();
            return modelDeletePredicate;
        }

        private void WhenNoMessageInsert()
        {
            _model.Save(_order, Arg.Any<Action<Order>>(), Arg.Any<Action<Order>>());
            _orderController.Save(_order);
        }

        private void ShouldNotReceiveMessage()
        {
            _log.DidNotReceive().Write(Arg.Any<string>());
        }

        private void WhenUpdateMessage()
        {
            _model.Save(_order, Arg.Any<Action<Order>>(), Arg.Invoke(_order));
            _orderController.Save(_order);
        }

        private void ShouldReceivedMessageContains(string update)
        {
            _log.Received(1).Write(Arg.Is<string>(x => x.Contains(update)));
        }

        private void CreateOrderWhenCustomerAgeMoreThen(int age)
        {
            _order = new Order {Id = 91, Amount = 100, Customer = new Customer() {Age = age}};
        }

        private void CreateOrder()
        {
            _order = new Order {Id = 91, Amount = 100};
        }
    }
}