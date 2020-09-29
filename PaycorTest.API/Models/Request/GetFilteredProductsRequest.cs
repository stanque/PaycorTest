using PaycorTest.Common.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaycorTest.API.Models.Request
{
    public class GetFilteredProductsRequest
    {
        public string ProductName { get; set; }
        public DateTime? SellDateTime { get; set; }
        [Required]
        public PaginationData PaginationData { get; set; }
        public List<string> Keywords { get; set; }
    }
}
