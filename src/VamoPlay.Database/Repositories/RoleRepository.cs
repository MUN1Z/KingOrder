using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Interfaces.Repositories;
using VamoPlay.Database.Contexts;

namespace VamoPlay.Database.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        #region constructors

        public RoleRepository(VamoPlayContext context) : base(context)
        {
        }

        #endregion constructors
    }
}
