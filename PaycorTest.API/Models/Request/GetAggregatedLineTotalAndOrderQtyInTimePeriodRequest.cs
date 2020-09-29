using PaycorTest.Common.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaycorTest.API.Models.Request
{
    public class GetAggregatedLineTotalAndOrderQtyInTimePeriodRequest
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public PaginationData PaginationData { get; set; }
    }
}
