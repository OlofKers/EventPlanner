namespace EventPlanner.Models
{
    public class Gathering
    {
        public int GatheringId { get; set; }
        public string? GatheringName { get; set; }
        public string? GatheringDesc { get; set; }
        public bool MinorsAllowed { get; set; }
        public DateTime GatheringStart {  get; set; }
        public DateTime GatheringEnd { get; set; }

        public Category? GatheringCategory { get; set; }
        public IEnumerable<Registration>? Registrations { get; set; }

        
    }
}
