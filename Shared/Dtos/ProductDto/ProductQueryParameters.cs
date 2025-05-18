using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.ProductDto
{
    public class ProductQueryParameters
    {
        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;

        public ProductSortOption? SortOption { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;

        private int _pageSize = DefaultPageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 0 && value <MaxPageSize ? value : DefaultPageSize;
        }

    }
}
