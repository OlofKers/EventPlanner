namespace EventPlanner.Models
{
    public class Gathering
    {
        
        public int GatheringId { get; set; }
        public string? GatheringName { get; set; }
        public string? GatheringDesc { get; set; }
        public bool MinorsAllowed { get; set; }
        public DateOnly GatheringStart {  get; set; }
        public DateOnly GatheringEnd { get; set; }

        public int CategoryId { get; set; }
        public Category? GatheringCategory { get; set; }
        public IEnumerable<Registration>? Registrations { get; set; }

        
    }
}
