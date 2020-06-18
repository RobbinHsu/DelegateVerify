using NSubstitute;
using NUnit.Framework;

namespace DelegateVerify
{
    [TestFixture]
    public class OrderControllerTests
    {
        [Test]
        public void exist_order_should_update()
        {
            //TODO
            var model = Substitute.For<IOrderModel>();
            var orderController = new OrderController(model);

            orderController.Save(new Order {Id = 91, Amount = 100});
        }


        [Test]
        public void no_exist_order_should_insert()
        {
            //TODO
            var model = Substitute.For<IOrderModel>();
            var orderController = new OrderController(model);

            orderController.Save(new Order {Id = 91, Amount = 100});
        }

        [Test]
        public void test_delete_adult_orders()
        {
            //TODO
            var model = Substitute.For<IOrderModel>();
            var orderController = new OrderController(model);
            orderController.DeleteAdultOrders();
        }
    }
}