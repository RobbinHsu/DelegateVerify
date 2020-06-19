using NSubstitute;
using NUnit.Framework;

namespace DelegateVerify
{
    [TestFixture]
    public class OrderModelTests
    {
        private IRepository<Order> _repository = Substitute.For<IRepository<Order>>();

        [Ignore("")]
        [Test]
        public void insert_order()
        {
            //TODO
            var myOrderModel = new MyOrderModel(_repository);
        }

        [Ignore("")]
        [Test]
        public void update_order()
        {
            //TODO
            var myOrderModel = new MyOrderModel(_repository);
        }
    }
}