using System;

namespace DelegateVerify
{
    public class MyOrderModel : IOrderModel
    {
        private readonly IRepository<Order> _repository;

        public MyOrderModel(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public void Save(Order order, Action<Order> insertCallback, Action<Order> updateCallback)
        {
            if (!_repository.IsExist(order))
            {
                if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
                {
                    order.Amount += 100;
                }
                _repository.Insert(order);
                insertCallback(order);
            }
            else
            {
                _repository.Update(order);
                updateCallback(order);
            }
        }

        public void Delete(Func<Order, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}