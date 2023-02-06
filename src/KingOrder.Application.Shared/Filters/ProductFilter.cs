namespace KingOrder.Application.Filters
{
    public class ProductFilter : PaginationFilter
    {
        #region properties

        public string? Name { get; set; }
        public bool? Favorite { get; set; }

        #endregion

        #region constructors

        public ProductFilter() : base()
        {
            Name = null;
            Favorite = null;
        }

        public ProductFilter(string? name = "", bool? favorite = null) : base()
        {
            Name = name;
            Favorite = favorite;
        }
        #endregion
    }
}
