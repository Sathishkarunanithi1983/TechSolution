using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tech.Models
{
    public class DataTableResponse
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<UserDetail> data { get; set; }
        public string error { get; set; }
    }
}