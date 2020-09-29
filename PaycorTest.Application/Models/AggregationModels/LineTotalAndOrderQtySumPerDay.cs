using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Application.Models.AggregationModels
{
    public class LineTotalAndOrderQtySumPerDay
    {
        public DateTime Date { get; set; }
        public decimal LineTotalSum { get; set; }
        public int OrderQtySum { get; set; }
    }
}
