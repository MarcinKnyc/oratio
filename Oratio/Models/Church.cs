namespace Oratio.Models
{
    public class Church
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public Guid ParishId { get; set; }
        public Parish? Parish { get; set; } 
        public Address? Address { get; set; }
    }
}
