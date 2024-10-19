using AutoMapper;
using Flex.Core.Models.Common;
using Flex.Core.Shared.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Flex.Core.Extensions
{
    public static class IQueryableExtension
    {
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
                var sorting = $"{request.OrderBy} {request.SortBy}";
                query = query.OrderBy(sorting);
            }

            var items = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .ToListAsync();

            return PageResult<T>.CreatePagedResult(request.PageIndex,request.PageSize,request.OrderBy,request.SortBy,totalItems,items);
        }

        public static async Task<PageResult<TDestination>> ToPageResultAsync<TSource, TDestination>(this IQueryable<TSource> query,PagedRequest request,IMapper mapper) // IMapper là tùy chọn
        {
            // Đếm tổng số phần tử
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

            // Lấy các phần tử trong trang hiện tại
            var items = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .ToListAsync();

            // Kiểm tra xem có cần map hay không
            IList<TDestination> resultItems;
            if (typeof(TSource) == typeof(TDestination) || mapper == null)
            {
                // Nếu TSource và TDestination giống nhau hoặc không có mapper, trả về nguyên bản
                resultItems = items.Cast<TDestination>().ToList();
            }
            else
            {
                // Thực hiện map từ TSource sang TDestination
                resultItems = mapper.Map<IList<TDestination>>(items);
            }

            // Trả về đối tượng PageResult<TDestination>
            return PageResult<TDestination>.CreatePagedResult(
                request.PageIndex,
                request.PageSize,
                request.OrderBy,
                request.SortBy,
                totalItems,
                resultItems);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            return query.Skip((pagedRequest.PageIndex - 1) * pagedRequest.PageSize)
                        .Take(pagedRequest.PageSize);
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
