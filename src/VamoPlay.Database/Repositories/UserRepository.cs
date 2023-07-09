using VamoPlay.Database.Contexts;
using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Interfaces.Repositories;

namespace VamoPlay.Database.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(VamoPlayContext context) : base(context)
        {
        }
    }
}
