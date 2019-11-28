using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.WebApi;
using RoskildeTasks.Api.Models;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Web;

namespace RoskildeTasks.Api.Controllers
{
    public class MessageController : UmbracoApiController
    {
        [RoleAuthorize]
        [HttpGet]
        public IHttpActionResult GetMessagesForTask(int taskId)
        {
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            var userUdi = ms.GetByUsername(currentUser).GetUdi().ToString();

            IContentService cs = Services.ContentService;

            var task = cs.GetById(taskId);

            if(task != null && task.ContentTypeId == Configurations.TaskDocType)
            {
                var allMessages = cs.GetContentOfContentType(Configurations.MessageDocType);

                List<TaskMessageItem> messages = new List<TaskMessageItem>();

                foreach (var message in allMessages)
                {
                    var messageMember = message.GetValue("member").ToString();

                    if (userUdi == messageMember)
                    {
                        var taskUri = message.GetValue("task");
                        if (taskUri != null)
                        {
                            var memberTaskId = Umbraco.TypedContent(taskUri).Id;
                            if (taskId == memberTaskId)
                            {
                                TaskMessageItem taskMessage = new TaskMessageItem();
                                taskMessage.MemberUdi = messageMember;
                                taskMessage.Content = message.GetValue("content").ToString();
                                taskMessage.isFromAdmin = message.GetValue<bool>("isFromAdmin");
                                taskMessage.TaskID = memberTaskId;
                                taskMessage.Date = message.CreateDate;
                                messages.Add(taskMessage);
                            }
                        }
                    }
                }
                if(messages.Any())
                {
                    return Ok(messages);
                }
                else
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                
            }
            else
            {
                return BadRequest();
            }


        }

        [RoleAuthorize]
        [HttpPost]
        public IHttpActionResult SubmitMessageForTask()
        {
            string Json = Functions.GetJsonFromStream(HttpContext.Current.Request.InputStream);
            Models.DTO.TaskMessageItem message = new Models.DTO.TaskMessageItem();

            try
            {
                message = JsonConvert.DeserializeObject<Models.DTO.TaskMessageItem>(Json);
            }
            catch
            {
                return BadRequest();
            }

            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            var userUdi = ms.GetByUsername(currentUser).GetUdi().ToString();

            IContentService cs = Services.ContentService;

            string taskUdi;

            try
            {
                var task = cs.GetById(message.TaskId);
                
                if (task.ContentTypeId == Configurations.TaskDocType)
                {
                    taskUdi = task.GetUdi().ToString();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }

            var messageParent = cs.GetById(1086).GetUdi();

            var newMessage = cs.CreateContent("message", messageParent, "Message");

            newMessage.SetValue("member", userUdi);
            newMessage.SetValue("content", message.Content);
            newMessage.SetValue("isFromAdmin", false);
            newMessage.SetValue("task", taskUdi);

            try
            {
                cs.Save(newMessage);
                cs.Publish(newMessage);

                return Content(HttpStatusCode.Created, newMessage);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [RoleAuthorize]
        [HttpGet]
        public IHttpActionResult GetMessagesForCategory(int categoryId)
        {
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            var userUdi = ms.GetByUsername(currentUser).GetUdi().ToString();

            IContentService cs = Services.ContentService;

            var category = cs.GetById(categoryId);

            if(category != null && category.ContentTypeId == Configurations.CategoryDocType)
            {
                var allMessages = cs.GetContentOfContentType(Configurations.MessageDocType);

                List<MessageItem> messages = new List<MessageItem>();

                foreach (var message in allMessages)
                {
                    var messageMember = message.GetValue("member").ToString();

                    if (userUdi == messageMember)
                    {
                        var categoryUri = message.GetValue("category");
                        if (categoryUri != null)
                        {
                            var memberCategoryId = Umbraco.TypedContent(categoryUri).Id;
                            if (categoryId == memberCategoryId)
                            {
                                MessageItem categoryMessage = new MessageItem();
                                categoryMessage.MemberUdi = messageMember;
                                categoryMessage.Content = message.GetValue("content").ToString();
                                categoryMessage.Date = message.CreateDate;
                                messages.Add(categoryMessage);
                            }
                        }
                    }
                }
                if(messages.Any())
                {
                    return Ok(messages);
                }
                else
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                
            }

            else
            {
                return BadRequest();
            }



        }

        [RoleAuthorize]
        [HttpPost]
        public IHttpActionResult SubmitMessageForCategory()
        {
            string Json = Functions.GetJsonFromStream(HttpContext.Current.Request.InputStream);

            Models.DTO.CatgoryMessageItem message = new Models.DTO.CatgoryMessageItem();

            try
            {
                message = JsonConvert.DeserializeObject<Models.DTO.CatgoryMessageItem>(Json);
            }
            catch
            {
                return BadRequest("Json parser error:" + Json);
            }

            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            var userUdi = ms.GetByUsername(currentUser).GetUdi().ToString();

            IContentService cs = Services.ContentService;

            string categoryUdi;
            try
            {
                var category = cs.GetById(message.CategoryId);
                if(category.ContentTypeId == Configurations.CategoryDocType)
                {
                    categoryUdi = category.GetUdi().ToString();
                }
                else
                {
                    return BadRequest("CategoryId is not a category");
                }
            }
            catch
            {
                return BadRequest("CategoryId is not a node");
            }
            

            var messageParent = cs.GetById(Configurations.MessageNode).GetUdi();

            var newMessage = cs.CreateContent("message", messageParent, "Message");

            newMessage.SetValue("member", userUdi);
            newMessage.SetValue("content", message.Content);
            newMessage.SetValue("isFromAdmin", false);
            newMessage.SetValue("category", categoryUdi);

            try
            {
                cs.Save(newMessage);
                cs.Publish(newMessage);

                return Content(HttpStatusCode.Created, newMessage);
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
