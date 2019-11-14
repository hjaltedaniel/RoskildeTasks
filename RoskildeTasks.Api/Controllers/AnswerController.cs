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
            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();
            string Json = bodyText;

            Models.AnswerSend.Root Answer = JsonConvert.DeserializeObject<Models.AnswerSend.Root>(Json);
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            var user = ms.GetByUsername(currentUser);
            var userUdi = user.GetUdi().ToString();

            IContentService cs = Services.ContentService;

            var task = cs.GetById(Answer.TaskId);
            var taskUdi = task.GetUdi().ToString();

            var answerParent = cs.GetById(1135).GetUdi();
            string answerTitle = task.Name + " - " + user.Name;

            var answerInDb = cs.GetChildrenByName(1135, answerTitle);

            if(answerInDb.Any())
            {
                foreach(var dbAnswer in answerInDb)
                {
                    var singleNewAnswer = cs.CreateContent("answer", dbAnswer.GetUdi(), "SingleAnswer");
                    var archetype = new ArchetypeModel();
                    var fieldsets = new List<ArchetypeFieldsetModel>();

                    foreach (Models.AnswerSend.Item row in Answer.Rows)
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
                            intProp.Value = row.Content;
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

                foreach (Models.AnswerSend.Item row in Answer.Rows)
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

            return StatusCode(HttpStatusCode.OK);
        }
    }
}
