using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tech.Models
{
    public class ClsResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public object result { get; set; }
    }
}