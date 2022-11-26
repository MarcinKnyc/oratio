using Microsoft.Extensions.Hosting;

namespace Oratio.Models
{
    public class Parish : Entity
    {
        public string Name { get; set; } = "";
        public string Dedicated { get; set; } = "";
        public List<Church> Churches { get; set; } = new List<Church>();

        public float? MinimumOffering { get; set; }

    }
}
