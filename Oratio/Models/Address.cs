namespace Oratio.Models
{
    public class Address: Entity
    {
        public String StreetName { get; set; } = "";
        public int StreetNumber { get; set; }
        public String City { get; set; } = "";
        public String ZipCode { get; set; } = "";
        public Guid ChurchId { get; set; }
        public Church? Church { get; set; }
    }
}
