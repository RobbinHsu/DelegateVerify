namespace DelegateVerify
{
    public class Order
    {
        public Order(int id, int amount)
        {
            Id = id;
            Amount = amount;
        }

        public Order()
        {
        }

        public int Amount { get; set; }
        public int Id { get; set; }
        public Customer Customer { get; set; }
    }

    public class Customer
    {
        public int Age { get; set; }
    }
}