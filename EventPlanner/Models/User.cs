namespace EventPlanner.Models
{
    public class User
    {
        
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
        public int UserAge {  get; set; }
        public string? UserRole { get; set; }
        public IEnumerable<Suggestion>? Suggestions { get; set; }
        public IEnumerable<Registration>? Registrations { get; set; }

    }
}
