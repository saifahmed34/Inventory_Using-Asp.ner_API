using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class itemmdls
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public double Price { get; set; }
        public string? Notes { get; set; }

        public IFormFile Images { get; set; }

        public int CategoryId { get; set; }
    }
}
