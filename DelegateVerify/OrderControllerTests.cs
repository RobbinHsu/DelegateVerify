using System;
using NSubstitute;
using NUnit.Framework;

namespace DelegateVerify
{
    [TestFixture]
    public class OrderControllerTests
    {
        private ILog _log;
        private IOrderModel _model;
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
            var order = new Order {Id = 91, Amount = 100};
            _model.Save(order, Arg.Any<Action<Order>>(), Arg.Invoke(order));
            _orderController.Save(order);

            _log.Received(1).Write(Arg.Is<string>(x => x.Contains("update")));
        }

        [Test]
        public void no_exist_order_should_insert()
        {
            var order = new Order {Id = 91, Amount = 100};
            _model.Save(order, Arg.Any<Action<Order>>(), Arg.Any<Action<Order>>());
            _orderController.Save(order);

            _log.DidNotReceiveWithAnyArgs();
        }

        [Test]
        public void test_delete_adult_orders()
        {
            var order = new Order {Id = 91, Amount = 100, Customer = new Customer() {Age = 30}};

            Func<Order, bool> modelDeletePredicate = null;

            _model.When(m => m.Delete(Arg.Any<Func<Order, bool>>()))
                .Do(m => modelDeletePredicate = m[0] as Func<Order, bool>);

            _orderController.DeleteAdultOrders();

            Assert.IsTrue(modelDeletePredicate(order));
        }
    }
}