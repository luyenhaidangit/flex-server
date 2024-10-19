using System.ComponentModel.DataAnnotations;

namespace Flex.Core.Models.Common
{
    public abstract class PagedRequest
    {
        private const int MaxPageSize = 500;

        private int _pageIndex = 1;

        [Range(1, int.MaxValue, ErrorMessage = "PageNumber phải lớn hơn 0.")]
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = (value <= 0) ? 1 : value;
        }

        [Range(1, MaxPageSize, ErrorMessage = "PageSize phải từ 1 đến {1}.")]
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? OrderBy { get; set; }
        public string? SortBy { get; set; }
    }
}
