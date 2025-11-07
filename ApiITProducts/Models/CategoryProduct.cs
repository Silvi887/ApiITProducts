namespace ApiITProducts.Models
{
    public class CategoryProduct
    {

        public int Productid { get; set; }


        public Product Product { get; set; } = null!;

        public int Categoryid { get; set; }

        public Category Category { get; set; } = null!;
    }
}
