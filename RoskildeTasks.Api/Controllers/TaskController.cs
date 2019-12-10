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
using Newtonsoft.Json.Linq;

namespace RoskildeTasks.Api.Controllers
{
    public class TaskController : UmbracoApiController
    {
        [RoleAuthorize]
        [HttpGet]
        public IHttpActionResult GetAllTasks()
        {
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            IContentService cs = Services.ContentService;

            var everyTask = cs.GetContentOfContentType(Configurations.TaskDocType);
            List<TaskItem> userTasks = new List<TaskItem>();

            if (everyTask.Any())
            {
                foreach (var task in everyTask)
                {

                    var taskMembers = task.GetValue("members");
                    string taskGroups;
                    try
                    {
                        taskGroups = taskMembers.ToString();
                    }
                    catch
                    {
                        continue;
                    }

                    if (Functions.IsMemberInGroups(taskGroups, currentUser))
                    {
                        TaskItem usersTask = new TaskItem();
                        usersTask.id = task.Id;
                        usersTask.name = task.Name;
                        usersTask.deadline = DateTime.Parse(task.GetValue("deadline").ToString());
                        usersTask.description = task.GetValue("description").ToString();
                        
                        var editorUri = task.GetValue("editor");
                        var thisEditor = cs.GetById(Umbraco.TypedContent(editorUri).Id);
                        JObject columns = Functions.ConvertEditorToJsonObject(thisEditor.GetValue("editorProperties").ToString());

                        string editorType;

                        if(thisEditor.GetValue("editorType").ToString() == "104")
                        {
                            editorType = "file";
                        }
                        else if(thisEditor.GetValue("editorType").ToString() == "105")
                        {
                            editorType = "list";
                        }
                        else
                        {
                            editorType = null;
                        }

                        JObject editor = new JObject();
                        editor.Add("type", editorType);
                        if(editorType == "list")
                        {
                            editor.Add("columns", columns);
                        }

                        usersTask.editor = editor;

                        var categoryUri = task.GetValue("category");
                        var thisCategory = cs.GetById(Umbraco.TypedContent(categoryUri).Id);
                        JObject category = new JObject();
                        category.Add("id", thisCategory.Id);
                        category.Add("name", thisCategory.Name);
                        var colorString = thisCategory.GetValue("categoryColor").ToString();
                        ColorItem color = JsonConvert.DeserializeObject<ColorItem>(colorString);
                        category.Add("color", "#" + color.Value);
                        usersTask.category = category;

                        userTasks.Add(usersTask);
                    }

                }
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            if(userTasks.Any())
            {
                return Ok(userTasks);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

        }
        [RoleAuthorize]
        [HttpGet]
        public IHttpActionResult GetTask(int id)
        {
            var currentUser = Members.CurrentUserName;
            IContentService cs = Services.ContentService;

            var current = cs.GetById(id);

            if(current != null)
            {
                if (current.ContentType.Id == Configurations.TaskDocType)
                {
                    var taskMembers = current.GetValue("members").ToString();

                    if (Functions.IsMemberInGroups(taskMembers, currentUser))
                    {
                        TaskItem usersTask = new TaskItem();
                        usersTask.id = current.Id;
                        usersTask.name = current.Name;
                        usersTask.deadline = current.GetValue<DateTime>("deadline");
                        usersTask.description = current.GetValue("description").ToString();

                        var editorUri = current.GetValue("editor");
                        var thisEditor = cs.GetById(Umbraco.TypedContent(editorUri).Id);
                        JObject columns = Functions.ConvertEditorToJsonObject(thisEditor.GetValue("editorProperties").ToString());

                        string editorType;

                        if (thisEditor.GetValue("editorType").ToString() == "104")
                        {
                            editorType = "file";
                        }
                        else if (thisEditor.GetValue("editorType").ToString() == "105")
                        {
                            editorType = "list";
                        }
                        else
                        {
                            editorType = null;
                        }

                        JObject editor = new JObject();
                        editor.Add("type", editorType);
                        if (editorType == "list")
                        {
                            editor.Add("columns", columns);
                        }

                        usersTask.editor = editor;

                        var categoryUri = current.GetValue("category");
                        var thisCategory = cs.GetById(Umbraco.TypedContent(categoryUri).Id);
                        JObject category = new JObject();
                        category.Add("id", thisCategory.Id);
                        category.Add("name", thisCategory.Name);
                        var colorString = thisCategory.GetValue("categoryColor").ToString();
                        ColorItem color = JsonConvert.DeserializeObject<ColorItem>(colorString);
                        category.Add("color", "#" + color.Value);
                        usersTask.category = category;

                        return Ok(usersTask);
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.Unauthorized);
                    }

                }
                else
                {
                    return StatusCode(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}