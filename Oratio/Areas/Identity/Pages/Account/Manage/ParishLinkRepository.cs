using Oratio.Data;
using System.Security.Claims;

namespace Oratio.Areas.Identity.Pages.Account.Manage
{
    public class ParishLinkRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        public ParishLinkRepository(IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _context = dbContext;
        }

        public string getParishIdForLoggedUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var parishId = _context.Parishes.FirstOrDefault(parish => parish.OwnerId.ToString() == userId).Id;
            return parishId.ToString();
        }
    }
}
