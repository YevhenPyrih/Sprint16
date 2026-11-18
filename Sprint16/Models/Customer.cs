namespace Sprint16.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string First_name { get; set; }

        public string Last_name { get; set; }

        public string Address { get; set; }

        public int Discount { get; set; }

        public override string ToString()
        {
            return $"{First_name} {Last_name}";
        }

    }
}
