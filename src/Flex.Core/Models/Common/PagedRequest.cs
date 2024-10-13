namespace Flex.Core.Models.Common
{
    public class PagedRequest
    {
        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }

        public string? SortBy { get; set; }

        public string? OrderBy { get; set; }
    }
}
