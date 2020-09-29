using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Service.ServiceModels.AggregationModels
{
    public class LineTotalAndOrderQtySumPerDayDto
    {
        public DateTime Date { get; set; }
        public decimal LineTotalSum { get; set; }
        public int OrderQtySum { get; set; }
    }
}
