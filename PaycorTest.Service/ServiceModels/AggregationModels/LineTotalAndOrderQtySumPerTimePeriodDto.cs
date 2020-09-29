using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Service.ServiceModels.AggregationModels
{
    public class LineTotalAndOrderQtySumPerTimePeriodDto
    {
        public decimal LineTotalSumPerPeriod { get; set; }
        public int OrderQtySumPerPeriod { get; set; }
    }
}
