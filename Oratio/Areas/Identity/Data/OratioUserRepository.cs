using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oratio.Data;
using Oratio.Models;

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

        public void ActivateUserById(Guid id)
        {
            var user = FetchUserById(id);
            if (user is null) return;
            if (!user.IsAdministrator) return;
            OratioUser userNotNull = user;
            user.IsActive = true;            
            _userContext.Update(userNotNull);
            _userContext.SaveChanges();            
        }

        public void DeactivateUserById(Guid id)
        {
            var user = FetchUserById(id);
            if (user is null) return;
            OratioUser userNotNull = user;
            if (!user.IsAdministrator) return;
            user.IsActive = false;
            _userContext.Update(userNotNull);
            _userContext.SaveChanges();
        }
    }
}
