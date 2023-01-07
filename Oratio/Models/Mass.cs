namespace Oratio.Models
{
    public class Mass : Entity
    {
        public DateTime DateTime { get; set; }
        public Guid ChurchId { get; set; }
        public Church? Church { get; set; }
        public List<Intention> Intentions { get; set; } = new List<Intention>();
    }
}
