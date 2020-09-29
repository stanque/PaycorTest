using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.API.Models.AggregationModels
{
    public class LineTotalAndOrderQtySumPerDayViewModel
    {
        public DateTime Date { get; set; }
        public decimal LineTotalSum { get; set; }
        public int OrderQtySum { get; set; }
    }
}
