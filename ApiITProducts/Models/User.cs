using System.ComponentModel.DataAnnotations;

namespace ApiITProducts.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string LastName { get; set; } = null!;

        DateTime Dateofbirth { get; set; }

        public string? Town { get; set; }

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
    }
}
