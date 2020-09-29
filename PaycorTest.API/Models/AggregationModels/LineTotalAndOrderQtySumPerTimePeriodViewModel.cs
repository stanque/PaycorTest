using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.API.Models.AggregationModels
{
    public class LineTotalAndOrderQtySumPerTimePeriodViewModel
    {
        public decimal LineTotalSumPerPeriod { get; set; }
        public int OrderQtySumPerPeriod { get; set; }
    }
}
