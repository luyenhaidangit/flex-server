namespace Flex.Core.Models.Common
{
    public abstract class PagedRequest
    {
        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }

        private string? _orderBy;
        public string? OrderBy
        {
            get => _orderBy;
            set => _orderBy = value?.Trim().ToUpper();
        }

        private string? _sortBy;
        public string? SortBy
        {
            get => _sortBy;
            set => _sortBy = value?.Trim().ToUpper();
        }
    }
}
