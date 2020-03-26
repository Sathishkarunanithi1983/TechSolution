using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tech.Models
{
    public class UserDetail
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int GroupId { get; set; }
        public string groupname { get; set; }
        public Nullable<bool> UserStatus { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}