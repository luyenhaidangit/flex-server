using AutoMapper;
using Flex.Core.Models.Common;

namespace Flex.Core.Extensions
{
    public static class PagedResultExtension
    {
        public static PageResult<TDestination> MapTo<TSource, TDestination>(this PageResult<TSource> source, IMapper mapper)
        {
            var mapperItems = mapper.Map<IList<TDestination>>(source.Items);

            return PageResult<TDestination>.CreatePagedResult(source.PageIndex, source.PageSize, source.OrderBy, source.SortBy, source.TotalItems, mapperItems);
        }
    }
}
