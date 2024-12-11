namespace EventPlanner.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public User? RegistrationOwner { get; set; }
        public Gathering? RegistrationGathering { get; set; }
        public DateTime RegistrationCreated = DateTime.Now;

    }
}
