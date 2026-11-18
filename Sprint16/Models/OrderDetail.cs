namespace Sprint16.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }

        public float Quantity { get; set; }

    }
}
