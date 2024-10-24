using AutoMapper;
using Flex.Core.Models.Common;
using Flex.Core.Shared.Abstracts;
using Flex.Core.Shared.Locators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Dynamic.Core;

namespace Flex.Core.Extensions
{
    public static class IQueryableExtension
    {
        private static IMapper _mapper => ServiceLocator.Instance.GetRequiredService<IMapper>();

        public static async Task<PageResult<T>> ToPageResultAsync<T>(this IQueryable<T> query, PagedRequest request)
        {
            var totalItems = await query.CountAsync();

            // Make arrangements if applicable
            if (typeof(EntityBase).IsAssignableFrom(typeof(T)) && string.IsNullOrWhiteSpace(request.OrderBy))
            {
                query = query.OrderByDescending(e => ((EntityBase)(object)e).CreatedAt);
            }

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                var sorting = $"{request.OrderBy} {request.SortBy}".Trim();
                query = query.OrderBy(sorting);
            }

            // Paging
            if (request.PageIndex == null) request.PageIndex = 1;
            if (request.PageSize == null) request.PageSize = totalItems;

            var items = await query.Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
                                   .Take(request.PageSize.Value)
                                   .ToListAsync();

            return PageResult<T>.CreatePagedResult(request.PageIndex.Value,request.PageSize.Value,request.OrderBy,request.SortBy,totalItems,items);
        }

        public static async Task<PageResult<TDestination>> ToPageResultAsync<TSource, TDestination>(this IQueryable<TSource> query,PagedRequest request)
        {
            // Count the total number of elements
            var totalItems = await query.CountAsync();

            // Make arrangements if applicable
            if (typeof(EntityBase).IsAssignableFrom(typeof(TSource)) && string.IsNullOrWhiteSpace(request.OrderBy))
            {
                query = query.OrderByDescending(e => ((EntityBase)(object)e).CreatedAt);
            }

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                var sorting = $"{request.OrderBy} {request.SortBy}".Trim();
                query = query.OrderBy(sorting);
            }

            // Paging
            if (request.PageIndex == null) request.PageIndex = 1;
            if (request.PageSize == null) request.PageSize = totalItems;

            // Lấy các phần tử trong trang hiện tại
            var items = await query.Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
                                   .Take(request.PageSize.Value)
                                   .ToListAsync();

            // Kiểm tra xem có cần map hay không
            IList<TDestination> resultItems;
            if (typeof(TSource) == typeof(TDestination) || _mapper == null)
            {
                // Nếu TSource và TDestination giống nhau hoặc không có mapper, trả về nguyên bản
                resultItems = items.Cast<TDestination>().ToList();
            }
            else
            {
                // Thực hiện map từ TSource sang TDestination
                resultItems = _mapper.Map<IList<TDestination>>(items);
            }

            // Trả về đối tượng PageResult<TDestination>
            return PageResult<TDestination>.CreatePagedResult(
                request.PageIndex.Value,
                request.PageSize.Value,
                request.OrderBy,
                request.SortBy,
                totalItems,
                resultItems);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            return query.Skip((pagedRequest.PageIndex.Value - 1) * pagedRequest.PageSize.Value)
                        .Take(pagedRequest.PageSize.Value);
        }

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            if (!string.IsNullOrWhiteSpace(pagedRequest.OrderBy))
            {
                var sorting = $"{pagedRequest.OrderBy} {pagedRequest.SortBy}";
                query = query.OrderBy(sorting);
            }
            return query;
        }
    }
}
