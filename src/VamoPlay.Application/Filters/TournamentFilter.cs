namespace VamoPlay.Application.Filters
{
    public class TournamentFilter : PaginationFilter
    {
        #region properties

        public string? Name { get; set; }
        public bool? Favorite { get; set; }

        #endregion

        #region constructors

        public TournamentFilter() : base()
        {
            Name = null;
            Favorite = null;
        }

        public TournamentFilter(string? name = "", bool? favorite = null) : base()
        {
            Name = name;
            Favorite = favorite;
        }
        #endregion
    }
}
