namespace Flex.Core.Models.Common
{
    public class PageResult<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalItems / PageSize);
            }
        }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public string OrderBy { get; set; }
        public string SortBy { get; set; }
        public IList<T> Items { get; set; }

        public static PageResult<T> CreatePagedResult(int pageIndex,int pageSize,string order,string sort, int total, IList<T> items)
        {
            return new PageResult<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = total,
                OrderBy = order,
                SortBy = sort,
                Items = items
            };
        }
    }
}
