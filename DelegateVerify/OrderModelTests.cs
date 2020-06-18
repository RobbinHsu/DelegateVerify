using NSubstitute;
using NUnit.Framework;

namespace DelegateVerify
{
    [TestFixture]
    public class OrderModelTests
    {
        private bool _isInsert = false;
        private bool _isUpdate = false;
        private IRepository<Order> _repository = Substitute.For<IRepository<Order>>();

        [Test]
        public void insert_order()
        {
            var myOrderModel = new MyOrderModel(_repository);
            _repository.IsExist(Arg.Any<Order>()).ReturnsForAnyArgs(false);
            myOrderModel.Save(Arg.Any<Order>(),
                (o) => { _isInsert = true; },
                (o) => { _isUpdate = true; });

            Assert.IsFalse(_isUpdate);
            Assert.IsTrue(_isInsert);
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