using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssessment.Domain.Model
{   public class PagedResponse<T>
    {
        public PagedResponse(T data, int pageNumber, int pageSize, int totalRecords)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            Data = data;
            TotalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize);
        }

        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalPages { get; init; }
        public int TotalRecords { get; init; }
        public T Data { get; init; }
    }
}
