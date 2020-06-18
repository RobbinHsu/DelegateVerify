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

            ShouldInsertRepository();

            ShouldNotInvokeUpdateCallback();
            ShouldInvokeInsertCallback();
        }

        [Test]
        public void update_order()
        {
            GivenOrderExist();
            WhenSave();

            ShouldUpdateRepository();

            ShouldNotInvokeInsertCallback();
            ShouldInvokeUpdateCallback();
        }

        private void ShouldInsertRepository()
        {
            _repository.ReceivedWithAnyArgs(1).Insert(Arg.Any<Order>());
        }

        private void ShouldUpdateRepository()
        {
            _repository.ReceivedWithAnyArgs(1).Update(Arg.Any<Order>());
        }

        private void GivenOrderExist()
        {
            _repository.IsExist(Arg.Any<Order>()).ReturnsForAnyArgs(true);
        }

        private void ShouldInvokeUpdateCallback()
        {
            Assert.IsTrue(_isUpdate);
        }

        private void ShouldNotInvokeInsertCallback()
        {
            Assert.IsFalse(_isInsert);
        }

        private void ShouldInvokeInsertCallback()
        {
            Assert.IsTrue(_isInsert);
        }

        private void ShouldNotInvokeUpdateCallback()
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