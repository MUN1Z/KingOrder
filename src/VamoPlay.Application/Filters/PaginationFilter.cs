using VamoPlay.Application.Constants;
using VamoPlay.Domain.Shared.Interfaces;

namespace VamoPlay.Application.Filters
{
    public class PaginationFilter : IFilter
    {
        #region properties

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region constructors

        public PaginationFilter()
        {
            PageNumber = PaginationConstants.DefaultPageNumber;
            PageSize = PaginationConstants.DefaultPageSize;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        #endregion

        #region public methods implementations

        public void Validate()
        {
            PageNumber = PageNumber < PaginationConstants.DefaultPageNumber ? PaginationConstants.DefaultPageNumber : PageNumber;
            PageSize = PageSize < PaginationConstants.MinimumPageSize ? PaginationConstants.DefaultPageSize : PageSize;
        }

        public int GetPageNumber() => PageNumber;
        public int GetPageSize() => PageSize;

        #endregion
    }
}
