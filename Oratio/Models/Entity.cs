using Oratio.Areas.Identity.Data;

namespace Oratio.Models
{
    public class Entity
    {
        public Guid Id { get; set; }
        public Guid? OwnerId { get; set; }

    }
}
