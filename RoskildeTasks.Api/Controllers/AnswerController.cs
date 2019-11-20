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

            Models.DTO.AnswerRoot Answer = JsonConvert.DeserializeObject<Models.DTO.AnswerRoot>(Json);
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

            return StatusCode(HttpStatusCode.OK);
        }

        [RoleAuthorize]
        [HttpPost]
        public IHttpActionResult UpdateAnswer(int id)
        {
            string Json = Functions.GetJsonFromStream(HttpContext.Current.Request.InputStream);

            Models.DTO.AnswerRoot Answer = JsonConvert.DeserializeObject<Models.DTO.AnswerRoot>(Json);

            IContentService cs = Services.ContentService;

            var singleNewAnswer = cs.GetById(id);

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

            return StatusCode(HttpStatusCode.OK);
        }

        [RoleAuthorize]
        [HttpPost]
        public string SubmitFile()
        {
            HttpPostedFile upload = HttpContext.Current.Request.Files["file"];

            Stream inputStream = upload.InputStream;

            IMediaService ms = Services.MediaService;

            var name = Path.GetFileName(upload.FileName);

            IMedia file = ms.CreateMedia(name, Constants.System.Root, Constants.Conventions.MediaTypes.File);
            file.SetValue("umbracoFile", name, inputStream);

            ms.Save(file);

            return file.GetUdi().ToString();
        }
    }
}
