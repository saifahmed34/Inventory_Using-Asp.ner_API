namespace api.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }

        public List<Item> Items { get; set; }
    }
}
