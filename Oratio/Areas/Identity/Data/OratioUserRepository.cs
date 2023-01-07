using Oratio.Data;

namespace Oratio.Areas.Identity.Data
{
    public class OratioUserRepository
    {
        private readonly UserContext _userContext;
        public OratioUserRepository(UserContext userContext) 
        {
            _userContext = userContext;
        }

        public OratioUser? FetchUserById(Guid id)
        {
            return _userContext.Users.FirstOrDefault(user => user.Id == id.ToString());
        }
    }
}
