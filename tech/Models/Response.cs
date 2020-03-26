using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tech.Models
{
    public class Response
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object result { get; set; }
    }
}