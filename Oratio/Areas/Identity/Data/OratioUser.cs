using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Oratio.Areas.Identity.Data;

// Add profile data for application users by adding properties to the OratioUser class
public class OratioUser : IdentityUser
{
    public bool IsModerator { get; set; }
    public bool IsAdministrator { get; set; }
    public bool IsFaithful { get; set; }
    public bool IsActive { get; set; }
}

