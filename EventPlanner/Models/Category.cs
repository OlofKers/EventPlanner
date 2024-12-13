namespace EventPlanner.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDesc { get; set; }
        public IEnumerable<Gathering>? Gatherings { get; set; }

    }
}
