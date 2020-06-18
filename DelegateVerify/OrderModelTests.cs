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
        private IRepository<Order> _repository;

        [SetUp]
        public void Setup()
        {
            _isInsert = false;
            _isUpdate = false;
            _repository = Substitute.For<IRepository<Order>>();
            _myOrderModel = new MyOrderModel(_repository);
        }

        [Test]
        public void insert_order()
        {
            GivenOrderNoExist();
            WhenSave();

            ShouldNotInvokeInsertCallback();
            ShouldInvokeUpdateCallback();
        }

        [Test]
        public void update_order()
        {
            _repository.IsExist(Arg.Any<Order>()).ReturnsForAnyArgs(true);
            WhenSave();

            _repository.ReceivedWithAnyArgs(1).Update(Arg.Any<Order>());

            Assert.IsFalse(_isInsert);
            Assert.IsTrue(_isUpdate);
        }

        private void ShouldInvokeUpdateCallback()
        {
            Assert.IsTrue(_isInsert);
        }

        private void ShouldNotInvokeInsertCallback()
        {
            Assert.IsFalse(_isUpdate);
        }

        private void WhenSave()
        {
            var insertCallback = SimulateInsertCallback();
            var updateCallback = SimulateUpdateCallback();
            _myOrderModel.Save(Arg.Any<Order>(),
                insertCallback,
                updateCallback);
        }

        private Action<Order> SimulateUpdateCallback()
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