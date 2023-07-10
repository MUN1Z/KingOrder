using VamoPlay.Database.Contexts;
using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace VamoPlay.Database.Repositories
{
    public class TournamentRepository : BaseRepository<Tournament>, ITournamentRepository
    {
        public TournamentRepository(VamoPlayContext context) : base(context)
        {
        }
    }
}
