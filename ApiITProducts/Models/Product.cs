using System.ComponentModel.DataAnnotations;

namespace ApiITProducts.Models
{
    public class Product
    {
        public Product()
        {
            CategoriesProducts = new List<CategoryProduct>();
        }


        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Producer { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public string? picurl { get; set; }

        //public int SellerId { get; set; }
        //public User Seller { get; set; } = null!;

        //public int? BuyerId { get; set; }
        //public User? Buyer { get; set; } = null!;

        public ICollection<CategoryProduct> CategoriesProducts { get; set; }
        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
    }
}
