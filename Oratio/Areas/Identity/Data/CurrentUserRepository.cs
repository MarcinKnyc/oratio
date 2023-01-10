using Oratio.Data;
using Oratio.Models;
using System.Security.Claims;

namespace Oratio.Areas.Identity.Data
{
    public class CurrentUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly OratioUserRepository _oratioUserRepository;
        public CurrentUserRepository(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext,
            OratioUserRepository oratioUserRepository
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _context = dbContext;
            _oratioUserRepository = oratioUserRepository;
        }

        public bool isLoggedIn() => _httpContextAccessor.HttpContext is not null 
            && _httpContextAccessor.HttpContext.User is not null
            && _httpContextAccessor.HttpContext.User.Identity is not null
            && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public bool isLoggedInAsParish()
        {
            var user = getCurrentUser();
            if (user is null) return false;
            return user.IsAdministrator;
        }

        public bool isLoggedInAsFaithful()
        {
            var user = getCurrentUser();
            if (user is null) return false;
            return user.IsFaithful;
        }

        public bool isLoggedInAsModerator()
        {
            var user = getCurrentUser();
            if (user is null) return false;
            return user.IsModerator;
        }

        public OratioUser? getCurrentUser()
        {
            if (!isLoggedIn()) return null;

            Guid? currentUserId = getCurrentUserId();
            if (currentUserId == null) return null; //should never happen

            var user = _oratioUserRepository.FetchUserById((Guid)currentUserId);
            return user;
        }

        public Guid? getCurrentUserId() 
        {
            if (!isLoggedIn()) return null;
            return new Guid(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)); //warning is unsubstantiated; checked for null in isLoggedIn
        }

        public string getParishIdForLoggedUser()
        {
            if (!isLoggedIn()) return "";

            var userId = getCurrentUserId().ToString(); //if current user id not found, returns ""
            var parish = _context.Parishes.FirstOrDefault(parish => parish.OwnerId.ToString() == userId);
            var parishId = parish == null ?
                null :
                parish.Id.ToString();
            return parishId ?? "";
        }

        public Parish? getParishForLoggedUser()
        {
            if (!isLoggedIn() || !isLoggedInAsParish()) return null;

            var userId = getCurrentUserId().ToString(); //if current user id not found, returns ""
            var parish = _context.Parishes.FirstOrDefault(parish => parish.OwnerId.ToString() == userId);
            return parish;
        }
    }
}
