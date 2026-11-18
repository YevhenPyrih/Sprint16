namespace Sprint16.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }

        public Supermarket Supermarket { get; set; }

        public DateTime Order_date { get; set; }

        public override string ToString()
        {
            return $"{Id}";
        }

    }
}
