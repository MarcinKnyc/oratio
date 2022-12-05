namespace Oratio.Models
{
    public class Mass : Entity
    {
        public DateTime DateTime { get; set; }
        public Church? Church { get; set; }
    }
}
