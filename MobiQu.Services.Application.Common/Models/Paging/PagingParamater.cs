using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Application.Common.Models.Paging
{
    public class PagingParamater
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public PagingParamater(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
