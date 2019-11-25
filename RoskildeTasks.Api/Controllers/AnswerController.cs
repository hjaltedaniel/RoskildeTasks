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
using System.IO;
using Newtonsoft.Json;
using System.Net;
using Archetype.Models;
using System.Web;

namespace RoskildeTasks.Api.Controllers
{
    public class AnswerController : UmbracoApiController
    {
        [RoleAuthorize]
        [HttpPost]
        public IHttpActionResult SubmitAnswer()
        {
            string Json = Functions.GetJsonFromStream(HttpContext.Current.Request.InputStream);
            Models.DTO.AnswerRoot Answer = new Models.DTO.AnswerRoot();

            try
            {
                 Answer = JsonConvert.DeserializeObject<Models.DTO.AnswerRoot>(Json);
            }
            catch
            {
                return BadRequest();
            }

            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            var user = ms.GetByUsername(currentUser);
            var userUdi = user.GetUdi().ToString();

            IContentService cs = Services.ContentService;

            var task = cs.GetById(Answer.TaskId);

            if(task != null && task.ContentTypeId == Configurations.TaskDocType)
            {
                TaskItem currentTask = new TaskItem();
                currentTask.Id = task.Id;
                currentTask.Name = task.Name;
                currentTask.Description = task.GetValue("description").ToString();
                currentTask.Deadline = DateTime.Parse(task.GetValue("deadline").ToString());

                var editorUri = task.GetValue("editor");
                var thisEditor = cs.GetById(Umbraco.TypedContent(editorUri).Id);
                currentTask.Editor = Functions.ConvertToEditorItem(thisEditor.GetValue("editorProperties").ToString());

                var listComparison = Answer.Rows.Where(row =>currentTask.Editor.Any(editor =>editor.Name == row.Name && editor.ValueType == row.ValueType));
                bool isCorrectFormated = false;

                if(listComparison.Count() == currentTask.Editor.Count)
                {
                    isCorrectFormated = true;
                }

                if(isCorrectFormated)
                {
                    var taskUdi = task.GetUdi().ToString();

                    var answerParent = cs.GetById(Configurations.AnswerNode).GetUdi();
                    string answerTitle = task.Name + " - " + user.Name;

                    var answerInDb = cs.GetChildrenByName(Configurations.AnswerNode, answerTitle);

                    if (answerInDb.Any())
                    {
                        foreach (var dbAnswer in answerInDb)
                        {
                            var singleNewAnswer = cs.CreateContent("answer", dbAnswer.GetUdi(), "SingleAnswer");
                            var archetype = new ArchetypeModel();
                            var fieldsets = new List<ArchetypeFieldsetModel>();

                            foreach (Models.DTO.AnswerItem row in Answer.Rows)
                            {
                                var fieldset = new ArchetypeFieldsetModel();
                                fieldset.Alias = "column";
                                fieldset.AllowedMemberGroups = "";
                                fieldset.Disabled = false;
                                var properties = new List<ArchetypePropertyModel>();

                                var nameProp = new ArchetypePropertyModel();
                                nameProp.Alias = "name";
                                nameProp.Value = row.Name;
                                properties.Add(nameProp);

                                if (row.ValueType == "String")
                                {
                                    var stringProp = new ArchetypePropertyModel();
                                    stringProp.Alias = "string";
                                    stringProp.Value = row.Content;
                                    properties.Add(stringProp);

                                    var intProp = new ArchetypePropertyModel();
                                    intProp.Alias = "int32";
                                    intProp.Value = null;
                                    properties.Add(intProp);

                                    var fileProp = new ArchetypePropertyModel();
                                    fileProp.Alias = "file";
                                    fileProp.Value = null;
                                    properties.Add(fileProp);
                                }
                                else if (row.ValueType == "Int32")
                                {
                                    var stringProp = new ArchetypePropertyModel();
                                    stringProp.Alias = "string";
                                    stringProp.Value = null;
                                    properties.Add(stringProp);

                                    var intProp = new ArchetypePropertyModel();
                                    intProp.Alias = "int32";
                                    intProp.Value = row.Content;
                                    properties.Add(intProp);

                                    var fileProp = new ArchetypePropertyModel();
                                    fileProp.Alias = "file";
                                    fileProp.Value = null;
                                    properties.Add(fileProp);
                                }
                                else if (row.ValueType == "File")
                                {
                                    var stringProp = new ArchetypePropertyModel();
                                    stringProp.Alias = "string";
                                    stringProp.Value = null;
                                    properties.Add(stringProp);

                                    var intProp = new ArchetypePropertyModel();
                                    intProp.Alias = "int32";
                                    intProp.Value = null;
                                    properties.Add(intProp);

                                    var fileProp = new ArchetypePropertyModel();
                                    fileProp.Alias = "file";
                                    fileProp.Value = row.Content;
                                    properties.Add(fileProp);
                                }

                                fieldset.Properties = properties;
                                fieldsets.Add(fieldset);
                            }

                            archetype.Fieldsets = fieldsets;

                            singleNewAnswer.SetValue("content", JsonConvert.SerializeObject(archetype));

                            cs.Save(singleNewAnswer);
                        }
                    }
                    else
                    {
                        var newAnswer = cs.CreateContent(answerTitle, answerParent, "Answer");

                        newAnswer.SetValue("user", userUdi);
                        newAnswer.SetValue("task", taskUdi);

                        var singleNewAnswer = cs.CreateContent("answer", newAnswer, "SingleAnswer");
                        var archetype = new ArchetypeModel();
                        var fieldsets = new List<ArchetypeFieldsetModel>();

                        foreach (Models.DTO.AnswerItem row in Answer.Rows)
                        {
                            var fieldset = new ArchetypeFieldsetModel();
                            fieldset.Alias = "column";
                            fieldset.AllowedMemberGroups = "";
                            fieldset.Disabled = false;
                            var properties = new List<ArchetypePropertyModel>();

                            var nameProp = new ArchetypePropertyModel();
                            nameProp.Alias = "name";
                            nameProp.Value = row.Name;
                            properties.Add(nameProp);

                            if (row.ValueType == "String")
                            {
                                var stringProp = new ArchetypePropertyModel();
                                stringProp.Alias = "string";
                                stringProp.Value = row.Content;
                                properties.Add(stringProp);

                                var intProp = new ArchetypePropertyModel();
                                intProp.Alias = "int32";
                                intProp.Value = null;
                                properties.Add(intProp);

                                var fileProp = new ArchetypePropertyModel();
                                fileProp.Alias = "file";
                                fileProp.Value = null;
                                properties.Add(fileProp);
                            }
                            else if (row.ValueType == "Int32")
                            {
                                var stringProp = new ArchetypePropertyModel();
                                stringProp.Alias = "string";
                                stringProp.Value = null;
                                properties.Add(stringProp);

                                var intProp = new ArchetypePropertyModel();
                                intProp.Alias = "int32";
                                intProp.Value = row.Content;
                                properties.Add(intProp);

                                var fileProp = new ArchetypePropertyModel();
                                fileProp.Alias = "file";
                                fileProp.Value = null;
                                properties.Add(fileProp);
                            }

                            fieldset.Properties = properties;
                            fieldsets.Add(fieldset);
                        }

                        archetype.Fieldsets = fieldsets;

                        singleNewAnswer.SetValue("content", JsonConvert.SerializeObject(archetype));

                        cs.Save(newAnswer);
                        cs.Publish(newAnswer);
                        cs.Save(singleNewAnswer);
                    }

                    return StatusCode(HttpStatusCode.Created);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }

        [RoleAuthorize]
        [HttpPost]
        public IHttpActionResult UpdateAnswer(int id)
        {
            string Json = Functions.GetJsonFromStream(HttpContext.Current.Request.InputStream);

            Models.DTO.AnswerRoot Answer;

            try
            {
                Answer = JsonConvert.DeserializeObject<Models.DTO.AnswerRoot>(Json);
            }
            catch
            {
                return BadRequest();
            }

            IContentService cs = Services.ContentService;

            var singleNewAnswer = cs.GetById(id);

            if (singleNewAnswer != null && singleNewAnswer.ContentTypeId == Configurations.SingleAnswerDocType)
            {
                var taskUdi = singleNewAnswer.Parent().GetValue("task").ToString();
                var task = cs.GetById(Umbraco.TypedContent(taskUdi).Id);

                TaskItem currentTask = new TaskItem();
                currentTask.Id = task.Id;

                var editorUri = task.GetValue("editor");
                var thisEditor = cs.GetById(Umbraco.TypedContent(editorUri).Id);
                currentTask.Editor = Functions.ConvertToEditorItem(thisEditor.GetValue("editorProperties").ToString());

                if(Answer.TaskId == currentTask.Id)
                {

                    var listComparison = Answer.Rows.Where(row => currentTask.Editor.Any(editor => editor.Name == row.Name && editor.ValueType == row.ValueType));
                    bool isCorrectFormated = false;

                    if (listComparison.Count() == currentTask.Editor.Count)
                    {
                        isCorrectFormated = true;
                    }

                    if(isCorrectFormated)
                    {
                        var archetype = new ArchetypeModel();
                        var fieldsets = new List<ArchetypeFieldsetModel>();

                        foreach (Models.DTO.AnswerItem row in Answer.Rows)
                        {
                            var fieldset = new ArchetypeFieldsetModel();
                            fieldset.Alias = "column";
                            fieldset.AllowedMemberGroups = "";
                            fieldset.Disabled = false;
                            var properties = new List<ArchetypePropertyModel>();

                            var nameProp = new ArchetypePropertyModel();
                            nameProp.Alias = "name";
                            nameProp.Value = row.Name;
                            properties.Add(nameProp);

                            if (row.ValueType == "String")
                            {
                                var stringProp = new ArchetypePropertyModel();
                                stringProp.Alias = "string";
                                stringProp.Value = row.Content;
                                properties.Add(stringProp);

                                var intProp = new ArchetypePropertyModel();
                                intProp.Alias = "int32";
                                intProp.Value = null;
                                properties.Add(intProp);

                                var fileProp = new ArchetypePropertyModel();
                                fileProp.Alias = "file";
                                fileProp.Value = null;
                                properties.Add(fileProp);
                            }
                            else if (row.ValueType == "Int32")
                            {
                                var stringProp = new ArchetypePropertyModel();
                                stringProp.Alias = "string";
                                stringProp.Value = null;
                                properties.Add(stringProp);

                                var intProp = new ArchetypePropertyModel();
                                intProp.Alias = "int32";
                                intProp.Value = row.Content;
                                properties.Add(intProp);

                                var fileProp = new ArchetypePropertyModel();
                                fileProp.Alias = "file";
                                fileProp.Value = null;
                                properties.Add(fileProp);
                            }

                            fieldset.Properties = properties;
                            fieldsets.Add(fieldset);
                        }

                        archetype.Fieldsets = fieldsets;

                        singleNewAnswer.SetValue("content", JsonConvert.SerializeObject(archetype));

                        cs.Save(singleNewAnswer);

                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

            
        }

        [RoleAuthorize]
        [HttpPost]
        public IHttpActionResult SendAllAnswersForTask(int taskId)
        {

            IContentService cs = Services.ContentService;
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            var user = ms.GetByUsername(currentUser);
            var userUdi = user.GetUdi().ToString();

            var allAnswers = cs.GetById(Configurations.AnswerNode).Children();
            var realTask = cs.GetById(taskId);

            List<int> answerId = new List<int>();

            foreach (var answer in allAnswers)
            {
                var task = answer.GetValue("task");
                var answerTaskId = Umbraco.TypedContent(task).Id;

                var answerUser = answer.GetValue("user").ToString();

                if (answerTaskId == taskId && realTask.ContentTypeId == Configurations.TaskDocType && answerUser == userUdi)
                {
                    foreach (var child in answer.Children())
                    {
                        cs.Publish(child);
                    }
                    answerId.Add(answer.Id);
                    cs.Publish(answer);
                }
            }

            bool isTaskExisting = false;
            if (realTask != null && realTask.ContentTypeId == Configurations.TaskDocType)
            {
                isTaskExisting = true;
            }


            if(answerId.Any())
            {
                return Ok();
            }
            else if (isTaskExisting)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return BadRequest();
            }
        }

        [RoleAuthorize]
        [HttpPost]
        public IHttpActionResult SubmitFile()
        {
            HttpPostedFile upload = HttpContext.Current.Request.Files["file"];

            if(upload != null)
            {
                List<string> allowedFileTypes = new List<string>() { "pdf", "ai", "docx", "svg", "jpg", "png", "xlsx" };
                Stream inputStream = upload.InputStream;

                IMediaService ms = Services.MediaService;

                var filetype = Path.GetExtension(upload.FileName).Replace(".", "");

                if(allowedFileTypes.Contains(filetype))
                {
                    var name = Path.GetFileName(upload.FileName);

                    IMedia file = ms.CreateMedia(name, Constants.System.Root, Constants.Conventions.MediaTypes.File);
                    file.SetValue("umbracoFile", name, inputStream);

                    ms.Save(file);

                    return Content(HttpStatusCode.Created, file.GetUdi().ToString());
                }
                else
                {
                    return StatusCode(HttpStatusCode.Forbidden);
                }
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }


        }

        [RoleAuthorize]
        [HttpDelete]
        public IHttpActionResult DeleteSingleAnswer(int id)
        {

            IContentService cs = Services.ContentService;
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            var user = ms.GetByUsername(currentUser);
            var userUdi = user.GetUdi().ToString();

            var singleNewAnswer = cs.GetById(id);
            var parentAnswer = singleNewAnswer.Parent();
            var parentAnswerUser = parentAnswer.GetValue("user").ToString();

            if(parentAnswerUser == userUdi)
            {
                if (singleNewAnswer != null && singleNewAnswer.ContentTypeId == Configurations.SingleAnswerDocType)
                {
                    cs.Delete(singleNewAnswer);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Unauthorized();
            }




        }

        [RoleAuthorize]
        [HttpDelete]
        public IHttpActionResult DeleteAllAnswersForTask(int taskId)
        {

            IContentService cs = Services.ContentService;
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            var user = ms.GetByUsername(currentUser);
            var userUdi = user.GetUdi().ToString();

            var task = cs.GetById(taskId);

            if(task != null && task.ContentTypeId == Configurations.TaskDocType)
            {
                List<int> answerId = new List<int>();
                var allAnswers = cs.GetById(Configurations.AnswerNode).Children();
                foreach (var answer in allAnswers)
                {
                    var answerUser = answer.GetValue("user").ToString();
                    var answerTaskUdi = answer.GetValue("task");
                    var answerTaskId = Umbraco.TypedContent(answerTaskUdi).Id;

                    if (answerTaskId == task.Id && answerUser == userUdi)
                    {
                        foreach (var child in answer.Children())
                        {
                            cs.Delete(child);
                        }
                        answerId.Add(answer.Id);
                        cs.Delete(answer);
                    }
                }
                if(answerId.Any())
                {
                    return Ok();
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
    }
}
