using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Interfaces.Repositories;
using VamoPlay.Database.Contexts;

namespace VamoPlay.Database.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        #region constructors

        public UserRoleRepository(VamoPlayContext context) : base(context)
        {
        }

        #endregion constructors
    }
}
