using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaycorTest.Data.Entities
{
    [Table(name:"Culture", Schema = "Production")]
    public class Culture
    {
        [Key]
        public string CultureID { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
