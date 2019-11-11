using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.WebApi;
using RoskildeTasks.Api.Models;
using System.Net;

namespace RoskildeTasks.Api.Controllers
{
    public class TasksController : UmbracoApiController
    {

        [RoleAuthorize]
        [HttpGet]
        public List<TaskItem> GetAllTasks()
        {
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            IContentService cs = Services.ContentService;

            var everyTask = cs.GetContentOfContentType(1056);

            List<TaskItem> userTasks = new List<TaskItem>();

            foreach (var task in everyTask)
            {
                var taskGroup = task.GetValue("members").ToString();
                var allMembers = ms.GetMembersInRole(taskGroup);
                bool memberInGroup = false;
                foreach(var thisMember in allMembers)
                {
                    if (thisMember.Username == currentUser)
                    {
                        memberInGroup = true;
                    }
                }

                if(memberInGroup)
                {
                    TaskItem usersTask = new TaskItem();
                    usersTask.Id = task.Id;
                    usersTask.Name = task.Name;
                    usersTask.Description = task.GetValue("description").ToString();
                    usersTask.Deadline = DateTime.Parse(task.GetValue("deadline").ToString());

                    var categoryUri = task.GetValue("category");
                    var thisCategory = cs.GetById(Umbraco.TypedContent(categoryUri).Id);
                    usersTask.CategoryName = thisCategory.Name;

                    userTasks.Add(usersTask);
                }
            }

            return userTasks;
        }

        [RoleAuthorize]
        [HttpGet]
        public TaskItem GetTask(int taskId)
        {
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            IContentService cs = Services.ContentService;

            var everyTask = cs.GetContentOfContentType(1056);

            TaskItem currentTask = new TaskItem();

            foreach (var task in everyTask)
            {
                var taskGroup = task.GetValue("members").ToString();
                var allMembers = ms.GetMembersInRole(taskGroup);
                bool memberInGroup = false;
                foreach (var thisMember in allMembers)
                {
                    if (thisMember.Username == currentUser)
                    {
                        memberInGroup = true;
                    }
                }

                if (memberInGroup & task.Id == taskId)
                {
                    currentTask.Id = task.Id;
                    currentTask.Name = task.Name;
                    currentTask.Description = task.GetValue("description").ToString();
                    currentTask.Deadline = DateTime.Parse(task.GetValue("deadline").ToString());

                    var categoryUri = task.GetValue("category");
                    var thisCategory = cs.GetById(Umbraco.TypedContent(categoryUri).Id);
                    currentTask.CategoryName = thisCategory.Name;
                }
            }

            return currentTask;
        }

    }
}
