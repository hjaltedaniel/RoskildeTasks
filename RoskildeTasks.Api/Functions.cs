using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web.WebApi;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using RoskildeTasks.Api.Models;
using Newtonsoft.Json;
using Archetype.Models;
using System.IO;
using System.Web;
using Newtonsoft.Json.Linq;

namespace RoskildeTasks.Api
{
    public class Functions
    {
        public static bool IsMemberInGroups (string groups, string currentUser)
        {
            var ms = ApplicationContext.Current.Services.MemberService;

            bool memberInGroup = false;
            List<string> ressourceGroups = groups.Split(',').ToList();
            foreach (string ressourceGroup in ressourceGroups)
            {
                var allMembers = ms.GetMembersInRole(ressourceGroup);

                foreach (var thisMember in allMembers)
                {
                    if (thisMember.Username == currentUser)
                    {
                        memberInGroup = true;
                    }
                }
            }
            return memberInGroup;
        }
        public static List<EditorItem> ConvertToEditorItem(string json)
        {
            List<EditorItem> returnList  = new List<EditorItem>();

            var translationObject = JsonConvert.DeserializeObject<ArchetypeModel>(json);

            foreach (var property in translationObject.Fieldsets.Where(x => x != null && x.Properties.Any()))
            {
                var thisName = property.GetValue("fieldName");
                var thisType = property.GetValue("datatype");

                EditorItem editor = new EditorItem();
                editor.Name = thisName;
                editor.ValueType = GetTypeFromPrevalue(thisType);
                returnList.Add(editor);
            }
            return returnList;

        }
        public static JObject ConvertEditorToJsonObject(string json)
        {
            JObject columns = new JObject();

            var translationObject = JsonConvert.DeserializeObject<ArchetypeModel>(json);

            foreach (var property in translationObject.Fieldsets.Where(x => x != null && x.Properties.Any()))
            {
                var name = property.GetValue("fieldName");
                var displayName = property.GetValue("displayName");
                var thisType = property.GetValue("datatype");

                JObject editor = new JObject();
                editor.Add("displayName", displayName);
                editor.Add("type", GetTypeFromPrevalue(thisType));

                columns.Add(name, editor);
            }
            return columns;

        }
        public static string GetTypeFromPrevalue (string prevalue)
        {
            if(prevalue == "54")
            {
                return "file";
            }
            else if (prevalue == "52")
            {
                return "text";
            }
            else if (prevalue == "53")
            {
                return "number";
            }
            else
            {
                return null;
            }
        }

        public static string GetJsonFromStream (Stream stream)
        {
            var bodyStream = new StreamReader(stream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();
            string Json = bodyText;

            return Json;
        }
    }
}
