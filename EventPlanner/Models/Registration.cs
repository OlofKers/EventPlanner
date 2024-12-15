namespace EventPlanner.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public DateTime RegistrationCreated = DateTime.Now;

        public int UserId { get; set; }
        public User? RegistrationOwner { get; set; }

        public int GatheringId { get; set; }
        public Gathering? RegistrationGathering { get; set; }


    }
}
