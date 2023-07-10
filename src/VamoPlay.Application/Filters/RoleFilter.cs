namespace VamoPlay.Application.Filters
{
    public class RoleFilter : PaginationFilter
    {
        #region properties

        public string Name { get; set; }

        #endregion

        #region constructors

        public RoleFilter() : base()
        {
            Name = string.Empty;
        }

        public RoleFilter(int pageNumber, int pageSize, string name = "") : base()
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Name = name;
        }

        #endregion
    }
}
