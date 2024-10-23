namespace Flex.Core.Models.Common
{
    public abstract class PagedRequest
    {
        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }

        public string? OrderBy { get; set; }

        public string? SortBy { get; set; }
    }
}
