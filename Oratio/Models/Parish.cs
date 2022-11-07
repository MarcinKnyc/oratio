using Microsoft.Extensions.Hosting;

namespace Oratio.Models
{
    public class Parish
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Dedicated { get; set; } = "";
        public List<Church> Churches { get; set; } = new List<Church>();

    }
}
