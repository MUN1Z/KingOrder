namespace VamoPlay.Application.Filters
{
    public class UserFilter : PaginationFilter
    {
        #region properties

        public string Name { get; set; }

        #endregion

        #region constructors

        public UserFilter() : base()
        {
            Name = string.Empty;
        }

        public UserFilter(int pageNumber, int pageSize, string name = "") : base()
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Name = name;
        }

        #endregion
    }
}
