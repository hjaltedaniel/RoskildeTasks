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
using Archetype.Models;

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

                    var editorUri = task.GetValue("editor");
                    var thisEditor = cs.GetById(Umbraco.TypedContent(editorUri).Id);
                    usersTask.Editor = Functions.ConvertToEditorItem(thisEditor.GetValue("editorProperties").ToString());

                    var allAnswers = cs.GetContentOfContentType(1133);
                    foreach (var answer in allAnswers)
                    {
                        var answerTaskId = Umbraco.TypedContent(answer.GetValue("task")).Id;
                        if(answerTaskId == task.Id)
                        {
                            List<AnswerRootItem> answerRootList = new List<AnswerRootItem>();
                            var answerRows = answer.Children();
                            foreach(var row in answerRows)
                            {
                                AnswerRootItem answersForTask = new AnswerRootItem();
                                answersForTask.TaskId = answerTaskId;
                                answersForTask.isPublished = row.Published;
                                List<AnswerItem> answersList = new List<AnswerItem>();
                                var translationObject = JsonConvert.DeserializeObject<ArchetypeModel>(row.GetValue<string>("content"));

                                foreach (var property in translationObject.Fieldsets.Where(x => x != null && x.Properties.Any()))
                                {
                                    AnswerItem singleAnswer = new AnswerItem();
                                    singleAnswer.Name = property.GetValue("name");
                                    if(!string.IsNullOrWhiteSpace(property.GetValue("string")))
                                    {
                                        singleAnswer.Content = property.GetValue("string");
                                    }
                                    else if (!string.IsNullOrWhiteSpace(property.GetValue("int32")))
                                    {
                                        singleAnswer.Content = property.GetValue("int32");
                                    }
                                    else
                                    {
                                        singleAnswer.Content = null;
                                    }
                                    answersList.Add(singleAnswer);
                                }
                                answersForTask.Rows = answersList;
                                answerRootList.Add(answersForTask);
                            }
                            
                            usersTask.Answers = answerRootList;
                        }
                    }

                    var categoryUri = task.GetValue("category");
                    var thisCategory = cs.GetById(Umbraco.TypedContent(categoryUri).Id);
                    CategoryItem category = new CategoryItem();
                    category.Id = thisCategory.Id;
                    category.Name = thisCategory.Name;
                    category.ShortName = thisCategory.GetValue("shortName").ToString();
                    category.StandardMessage = thisCategory.GetValue("standardMessage").ToString();
                    category.isOnlyMessages = thisCategory.GetValue<bool>("isOnlyMessages");
                    var colorString = thisCategory.GetValue("categoryColor").ToString();
                    ColorItem color = JsonConvert.DeserializeObject<ColorItem>(colorString);
                    category.Color = color;

                    usersTask.Category = category;

                    if(usersTask.Answers == null)
                    {
                        usersTask.isDone = false;
                    }
                    else
                    {
                        if (usersTask.Answers.All(item => item.isPublished))
                        {
                            usersTask.isDone = true;
                        }
                        else
                        {
                            usersTask.isDone = false;
                        }
                    }



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

                    var editorUri = task.GetValue("editor");
                    var thisEditor = cs.GetById(Umbraco.TypedContent(editorUri).Id);
                    currentTask.Editor = Functions.ConvertToEditorItem(thisEditor.GetValue("editorProperties").ToString());

                    var allAnswers = cs.GetContentOfContentType(1133);
                    foreach (var answer in allAnswers)
                    {
                        var answerTaskId = Umbraco.TypedContent(answer.GetValue("task")).Id;
                        if (answerTaskId == task.Id)
                        {
                            List<AnswerRootItem> answerRootList = new List<AnswerRootItem>();
                            var answerRows = answer.Children();
                            foreach (var row in answerRows)
                            {
                                AnswerRootItem answersForTask = new AnswerRootItem();
                                answersForTask.TaskId = answerTaskId;
                                answersForTask.isPublished = row.Published;
                                List<AnswerItem> answersList = new List<AnswerItem>();
                                var translationObject = JsonConvert.DeserializeObject<ArchetypeModel>(row.GetValue<string>("content"));

                                foreach (var property in translationObject.Fieldsets.Where(x => x != null && x.Properties.Any()))
                                {
                                    AnswerItem singleAnswer = new AnswerItem();
                                    singleAnswer.Name = property.GetValue("name");
                                    if (!string.IsNullOrWhiteSpace(property.GetValue("string")))
                                    {
                                        singleAnswer.Content = property.GetValue("string");
                                    }
                                    else if (!string.IsNullOrWhiteSpace(property.GetValue("int32")))
                                    {
                                        singleAnswer.Content = property.GetValue("int32");
                                    }
                                    else
                                    {
                                        singleAnswer.Content = null;
                                    }
                                    answersList.Add(singleAnswer);
                                }
                                answersForTask.Rows = answersList;
                                answerRootList.Add(answersForTask);
                            }

                            currentTask.Answers = answerRootList;
                        }
                    }

                    var categoryUri = task.GetValue("category");
                    var thisCategory = cs.GetById(Umbraco.TypedContent(categoryUri).Id);
                    CategoryItem category = new CategoryItem();
                    category.Id = thisCategory.Id;
                    category.Name = thisCategory.Name;
                    category.ShortName = thisCategory.GetValue("shortName").ToString();
                    category.StandardMessage = thisCategory.GetValue("standardMessage").ToString();
                    category.isOnlyMessages = thisCategory.GetValue<bool>("isOnlyMessages");
                    var colorString = thisCategory.GetValue("categoryColor").ToString();
                    ColorItem color = JsonConvert.DeserializeObject<ColorItem>(colorString);
                    category.Color = color;

                    currentTask.Category = category;

                    if (currentTask.Answers.All(item => item.isPublished))
                    {
                        currentTask.isDone = true;
                    }
                    else
                    {
                        currentTask.isDone = false;
                    }
                }
            }

            return currentTask;
        }

    }
}