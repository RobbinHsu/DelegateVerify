namespace DelegateVerify
{
    public interface IRepository<T>
    {
        bool IsExist(Order order);
        void Insert(Order order);
        void Update(Order order);
    }
}