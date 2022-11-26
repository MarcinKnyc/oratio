using Oratio.Areas.Identity.Data;

namespace Oratio.Models
{
    public class Entity
    {
        public Guid Id { get; set; }
        public OratioUser Owner { get; set; }

    }
}
