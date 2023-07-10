namespace VamoPlay.Application.Filters
{
    public class UserRoleFilter : PaginationFilter
    {
        #region properties

        public string Name { get; set; }

        #endregion

        #region constructors

        public UserRoleFilter() : base()
        {
            Name = string.Empty;
        }

        public UserRoleFilter(int pageNumber, int pageSize, string name = "") : base()
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Name = name;
        }

        #endregion
    }
}
