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

        public event Action<Order> OnCreated;
        public event Action<Order> OnUpdated;

        public void Save(Order order)
        {
            if (!_repository.IsExist(order))
            {
                _repository.Insert(order);
                OnCreated?.Invoke(order);
            }
            else
            {
                _repository.Update(order);
                OnUpdated?.Invoke(order);
            }
        }

        public void Save(Order order, Action<Order> insertCallback, Action<Order> updateCallback)
        {
            if (!_repository.IsExist(order))
            {
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