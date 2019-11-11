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

namespace RoskildeTasks.Api.Controllers
{
    public class MessageController : UmbracoApiController
    {
        [RoleAuthorize]
        [HttpGet]
        public List<TaskMessageItem> GetMessagesForTask(int taskId)
        {
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            var userUdi = ms.GetByUsername(currentUser).GetUdi().ToString();

            IContentService cs = Services.ContentService;

            var allMessages = cs.GetContentOfContentType(1080);

            List<TaskMessageItem> messages = new List<TaskMessageItem>();

            foreach (var message in allMessages)
            {
                var messageMember = message.GetValue("member").ToString();

                if (userUdi == messageMember)
                {
                    var taskUri = message.GetValue("task");
                    var memberTaskId = Umbraco.TypedContent(taskUri).Id;
                    if(taskId == memberTaskId)
                    {
                        TaskMessageItem taskMessage = new TaskMessageItem();
                        taskMessage.MemberUdi = messageMember;
                        taskMessage.Content = message.GetValue("content").ToString();
                        taskMessage.TaskID = memberTaskId;
                        taskMessage.Date = message.CreateDate;
                        messages.Add(taskMessage);
                    }
                }
            }

            return messages;
        }
        [RoleAuthorize]
        [HttpPost]
        public IHttpActionResult SubmitMessageForTask(int taskId, string content)
        {
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            var userUdi = ms.GetByUsername(currentUser).GetUdi().ToString();

            IContentService cs = Services.ContentService;

            var taskUdi = cs.GetById(taskId).GetUdi().ToString();

            var messageParent = cs.GetById(1086).GetUdi();

            var newMessage = cs.CreateContent("message", messageParent, "Message");

            newMessage.SetValue("member", userUdi);
            newMessage.SetValue("content", content);
            newMessage.SetValue("task", taskUdi);

            cs.Save(newMessage);
            cs.Publish(newMessage);

            return StatusCode(HttpStatusCode.OK);
        }
    }
}
