using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tech.Models.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int GroupId { get; set; }
        public bool UserStatus { get; set; }
    }
}