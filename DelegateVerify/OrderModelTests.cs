using System;
using NSubstitute;
using NUnit.Framework;

namespace DelegateVerify
{
    [TestFixture]
    public class OrderModelTests
    {
        private bool _isInsert = false;
        private bool _isUpdate = false;
        private MyOrderModel _myOrderModel;
        private IRepository<Order> _repository = Substitute.For<IRepository<Order>>();

        [SetUp]
        public void Setup()
        {
            _myOrderModel = new MyOrderModel(_repository);
        }

        [Test]
        public void insert_order()
        {
            GivenOrderNoExist();
            WhenSave();

            ShouldInvokeInsertCallback();
            ShouldInvokeUpdateCallback();
        }

        [Ignore("")]
        [Test]
        public void update_order()
        {
            //TODO
            var myOrderModel = new MyOrderModel(_repository);
        }

        private void ShouldInvokeUpdateCallback()
        {
            Assert.IsTrue(_isInsert);
        }

        private void ShouldInvokeInsertCallback()
        {
            Assert.IsFalse(_isUpdate);
        }

        private void WhenSave()
        {
            var insertCallback = SimulateInsertCallback();
            var updateCallback = SumulateUpdateCallback();
            _myOrderModel.Save(Arg.Any<Order>(),
                insertCallback,
                updateCallback);
        }

        private Action<Order> SumulateUpdateCallback()
        {
            return (o) => { _isUpdate = true; };
        }

        private Action<Order> SimulateInsertCallback()
        {
            return (o) => { _isInsert = true; };
        }

        private void GivenOrderNoExist()
        {
            _repository.IsExist(Arg.Any<Order>()).ReturnsForAnyArgs(false);
        }
    }
}