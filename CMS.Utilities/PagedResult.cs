using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Utilities
{
    public class PagedResult<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public IList<T>? Data { get; set; }
        public PagedResult(List<T> data, int Count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(Count / (double)pageSize);
            AddRange(data);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static PagedResult<T> Create(List<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count;
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<T>(items, count, pageIndex, pageSize);
        }
    }
}
