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
using Newtonsoft.Json;

namespace RoskildeTasks.Api.Controllers
{
    public class TaskController : UmbracoApiController
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
                var taskGroups = task.GetValue("members").ToString();

                if(Functions.IsMemberInGroups(taskGroups, currentUser))
                {
                    TaskItem usersTask = new TaskItem();
                    usersTask.Id = task.Id;
                    usersTask.Name = task.Name;
                    usersTask.Description = task.GetValue("description").ToString();
                    usersTask.Deadline = DateTime.Parse(task.GetValue("deadline").ToString());

                    var categoryUri = task.GetValue("category");
                    var thisCategory = cs.GetById(Umbraco.TypedContent(categoryUri).Id);
                    CategoryItem category = new CategoryItem();
                    category.Name = thisCategory.Name;
                    category.ShortName = thisCategory.GetValue("shortName").ToString();
                    category.StandardMessage = thisCategory.GetValue("standardMessage").ToString();
                    category.isOnlyMessages = thisCategory.GetValue<bool>("isOnlyMessages");
                    var colorString = thisCategory.GetValue("categoryColor").ToString();
                    ColorItem color = JsonConvert.DeserializeObject<ColorItem>(colorString);
                    category.Color = color;

                    usersTask.Category = category;

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
                var taskGroups = task.GetValue("members").ToString();

                if (Functions.IsMemberInGroups(taskGroups, currentUser) & task.Id == taskId)
                {
                    currentTask.Id = task.Id;
                    currentTask.Name = task.Name;
                    currentTask.Description = task.GetValue("description").ToString();
                    currentTask.Deadline = DateTime.Parse(task.GetValue("deadline").ToString());

                    var categoryUri = task.GetValue("category");
                    var thisCategory = cs.GetById(Umbraco.TypedContent(categoryUri).Id);
                    CategoryItem category = new CategoryItem();
                    category.Name = thisCategory.Name;
                    category.ShortName = thisCategory.GetValue("shortName").ToString();
                    category.StandardMessage = thisCategory.GetValue("standardMessage").ToString();
                    category.isOnlyMessages = thisCategory.GetValue<bool>("isOnlyMessages");
                    var colorString = thisCategory.GetValue("categoryColor").ToString();
                    ColorItem color = JsonConvert.DeserializeObject<ColorItem>(colorString);
                    category.Color = color;

                    currentTask.Category = category;
                }
            }

            return currentTask;
        }

    }
}