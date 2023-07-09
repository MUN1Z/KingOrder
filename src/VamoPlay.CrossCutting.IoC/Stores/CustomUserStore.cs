using VamoPlay.CrossCutting.Auth.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VamoPlay.Database.Contexts;

namespace VamoPlay.CrossCutting.IoC.Stores
{
    public class CustomUserStore : UserStore<UserIdentity>
    {
        public CustomUserStore(VamoPlayContext context)
        : base(context)
        {
            // Disable AutoSaveChanges behavior by default
            AutoSaveChanges = false;
        }
    }
}
