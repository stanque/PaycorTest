using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaycorTest.Data.Entities
{
    [Table(name:"UnitMeasure", Schema = "Production")]
    public class UnitMeasure
    {
        [Key]
        public string UnitMeasureCode { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
