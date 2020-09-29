using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Application.Models.AggregationModels
{
    public class LineTotalAndOrderQtySumPerTimePeriod
    {
        public decimal LineTotalSumPerPeriod { get; set; }
        public int OrderQtySumPerPeriod { get; set; }
    }
}
