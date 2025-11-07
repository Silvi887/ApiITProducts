namespace ApiITProducts.Models
{
    public class Sale
    {

        public int Id { get; set; }

        // Foreign Key to Product
        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Foreign Key to User
        public int UserId { get; set; }
        public User User { get; set; }

        // Sale Date
        public DateTime SaleDate { get; set; }

        // Optional: Quantity and Total Price
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
