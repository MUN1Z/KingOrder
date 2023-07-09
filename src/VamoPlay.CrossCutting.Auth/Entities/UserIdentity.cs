using Microsoft.AspNetCore.Identity;
using VamoPlay.Domain.Entities;

namespace VamoPlay.CrossCutting.Auth.Entities
{
    public class UserIdentity : IdentityUser
    {
        public int AccountIdentityId { get; set; }

        public virtual User User { get; set; }
    }
}