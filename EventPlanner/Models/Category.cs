namespace EventPlanner.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDesc { get; set; }
        public IEnumerable<Gathering>? Gatherings { get; set; }

    }
}
