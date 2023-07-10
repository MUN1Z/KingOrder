using VamoPlay.Database.Contexts;
using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Interfaces.Repositories;

namespace VamoPlay.Database.Repositories
{
    public class TournamentCategoryRepository : BaseRepository<TournamentCategory>, ITournamentCategoryRepository
    {
        public TournamentCategoryRepository(VamoPlayContext context) : base(context)
        {
        }
    }
}
