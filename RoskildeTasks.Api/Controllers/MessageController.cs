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
                    if(taskUri != null)
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
            newMessage.SetValue("isFromAdmin", false);
            newMessage.SetValue("task", taskUdi);

            cs.Save(newMessage);
            cs.Publish(newMessage);

            return StatusCode(HttpStatusCode.OK);
        }

        [RoleAuthorize]
        [HttpGet]
        public List<MessageItem> GetMessagesForCategory(int categoryId)
        {
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            var userUdi = ms.GetByUsername(currentUser).GetUdi().ToString();

            IContentService cs = Services.ContentService;

            var allMessages = cs.GetContentOfContentType(1080);

            List<MessageItem> messages = new List<MessageItem>();

            foreach (var message in allMessages)
            {
                var messageMember = message.GetValue("member").ToString();

                if (userUdi == messageMember)
                {
                    var categoryUri = message.GetValue("category");
                    if(categoryUri != null)
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

            return messages;
        }

        [RoleAuthorize]
        [HttpPost]
        public IHttpActionResult SubmitMessageForCategory(int categoryId, string content)
        {
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            var userUdi = ms.GetByUsername(currentUser).GetUdi().ToString();

            IContentService cs = Services.ContentService;

            var categoryUdi = cs.GetById(categoryId).GetUdi().ToString();

            var messageParent = cs.GetById(1086).GetUdi();

            var newMessage = cs.CreateContent("message", messageParent, "Message");

            newMessage.SetValue("member", userUdi);
            newMessage.SetValue("content", content);
            newMessage.SetValue("isFromAdmin", false);
            newMessage.SetValue("category", categoryUdi);

            cs.Save(newMessage);
            cs.Publish(newMessage);

            return StatusCode(HttpStatusCode.OK);
        }
    }
}
