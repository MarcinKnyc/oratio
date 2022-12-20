namespace Oratio.Models
{
    public class Church : Entity
    {
        public string Name { get; set; } = "";
        public Guid ParishId { get; set; }
        public Parish? Parish { get; set; }
        public Address? Address { get; set; }
        public List<Mass> Masses { get; set; } = new List<Mass>();
    }
}
