
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tech.Models;

namespace tech.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        { 
            ViewBag.Title = "Home Page";
            return View();
        }
        public ActionResult GetUsers()
        {
            var lstUser = new List<UserDetail>();
            try
            {
                //Do To
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();            
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                //Paging Size (10,20,50,100)    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
              
                // Getting all Customer data                  
                //Search options   
                if (!string.IsNullOrEmpty(searchValue))
                {
                    using (var dbContext = new TechEntities())
                    {
                        lstUser = (from c in dbContext.Users
                                   where c.UserName.Contains(searchValue) || c.Group.GroupName.Contains(searchValue)
                                   select new UserDetail
                                   {
                                       UserId = c.UserId,
                                       UserName = c.UserName,
                                       groupname = c.Group.GroupName,
                                       GroupId = c.GroupId,
                                       UserStatus = c.UserStatus,
                                       CreatedDate = c.CreatedDate,
                                       UpdatedDate = c.UpdatedDate
                                   }).ToList();
                    }
                }
                else
                {
                    using (var dbContext = new TechEntities())
                    {
                        lstUser = (from c in dbContext.Users                                   
                                   select new UserDetail
                                   {
                                       UserId = c.UserId,
                                       UserName = c.UserName,
                                       groupname = c.Group.GroupName,
                                       GroupId = c.GroupId,
                                       UserStatus = c.UserStatus,
                                       CreatedDate = c.CreatedDate,
                                       UpdatedDate = c.UpdatedDate
                                   }).ToList();
                    }
                }
                recordsTotal = lstUser.Count();
                //Paging     
                var data = lstUser.Skip(skip).Take(pageSize).ToList();            
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult AddUser()
        {

            ViewBag.Title = "Add User";
            return View();
        }
        public ActionResult AddGroup()
        {

            ViewBag.Title = "Add Group";
            return View();
        }
    }
}
