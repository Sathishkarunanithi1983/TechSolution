using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using tech.Models;
using tech.Models.ViewModel;
using Newtonsoft.Json;

namespace tech.Controllers
{
    public class ValuesController : ApiController
    {
        #region User
        [HttpPost]        
        public DataTableResponse Employee(DataTableRequest request)
        {
            int recordsTotal = 0;
            HttpResponseMessage httpresponse = new HttpResponseMessage();            
            var lstUser = new List<UserDetail>();
            var lstTopUser = new List<UserDetail>();
            try
            {
                   /// var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                    //Paging Size (10,20,50,100)    
                    int pageSize = request.Length;
                    int skip = request.Start ;
                

                    // Getting all Customer data    


                //Sorting    
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                //{
                // customerData = lstUser.OrderBy(sortColumn + " " + sortColumnDir);
                //}
                //Search    
                if (!string.IsNullOrEmpty(request.Search.Value))
                    {
                    var searchText = request.Search.Value.Trim();
                    using (var dbContext = new TechEntities())
                        {
                            lstUser = (from c in dbContext.Users
                                       where c.UserName.Contains(searchText) || c.Group.GroupName.Contains(searchText)
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
                lstTopUser = lstUser.Skip(skip).Take(pageSize).ToList();
                    
            }
            catch (Exception ex)
            {
                ex.Message.ToString();               
            }
            return new DataTableResponse
            {
                draw = request.Draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsTotal,
                data = lstTopUser,
                error = ""
            };
            ///return Request.CreateResponse(HttpStatusCode.OK, new { draw = request.Draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lstTopUser });
        }

        [HttpPost]
        public HttpResponseMessage AddUser(UserViewModel users)
        {
            Response response = new Response();
            try
            {
                using (var dbContext = new TechEntities())
                {
                    var Req = dbContext.Users.FirstOrDefault(x => x.UserName == users.UserName);
                    if (Req == null)
                    {
                        var objuser = new User
                        {
                            UserName=users.UserName,
                            UserId= users.UserId,
                            GroupId=users.GroupId,
                            UserStatus = users.UserStatus,
                            CreatedDate=DateTime.Now
                        };
                        dbContext.Users.Add(objuser);
                        dbContext.SaveChanges();
                        var NewRecord = dbContext.Users.FirstOrDefault(x => x.UserName == users.UserName);
                        response.success = true;
                        response.result = NewRecord;
                        response.message = "User added successfully ";
                    }
                    else
                    {
                        response.success = false;
                        response.message = "User already exist";
                    }

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                response.success = false;
                response.message = "User adding failed, Please contact your administrator";
            }
            return Request.CreateResponse(HttpStatusCode.OK,response);
        }

        #endregion

        #region group
        [HttpPost]
        public HttpResponseMessage AddGroup(GroupViewModel groups)
        {
            HttpResponseMessage httpresponse = new HttpResponseMessage();
            Response response = new Response();
            try
            {
                using (var dbContext = new TechEntities())
                {
                    var Req = dbContext.Groups.FirstOrDefault(x => x.GroupName== groups.GroupName);
                    if (Req == null)
                    {
                        var objGroup = new Group {
                        GroupId=groups.GroupId,
                        GroupName=groups.GroupName,
                        GroupStatus=groups.GroupStatus
                        };
                        dbContext.Groups.Add(objGroup);
                        dbContext.SaveChanges();
                        var NewRecord = dbContext.Groups.FirstOrDefault(x => x.GroupName == groups.GroupName);
                        response.success = true;
                        response.result = NewRecord;
                        response.message = "Group added successfully ";
                    }
                    else
                    {
                        response.success = false;
                        response.message = "Group already exist";
                    }

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                response.success = false;
                response.message = "Group adding failed, Please contact your administrator";
            }
            return httpresponse;
        }

        [HttpGet]
        public HttpResponseMessage GetGroup()
        {
            HttpResponseMessage httpresponse = new HttpResponseMessage();
            Response response = new Response();
            try
            {
                using (var dbContext = new TechEntities())
                {
                    response.result = (from c in dbContext.Groups where c.GroupStatus==true select new {GroupId=c.GroupId,GroupName=c.GroupName }).ToList();
                    response.success = true;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                response.success = false;
                response.message = "Group get failed, Please contact your administrator";
            }
            return Request.CreateResponse(HttpStatusCode.OK,response);

        }

        #endregion


        #region others
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        #endregion
    }
}
