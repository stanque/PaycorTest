using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PaycorTest.Common.Result
{
    public class PaginationData
    {
        [DefaultValue(1)]
        public int CurrentPage { get; set; }
        [DefaultValue(10)]
        public int RowsPerPage { get; set; }
    }

    public class PaginationDataResult : PaginationData
    {
        public int PageCount { get; set; }
        public int RowCount { get; set; }
    }
}
