using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tech.Models
{
    public class DataTableRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DataTableSearch Search { get; set; }
    }
}