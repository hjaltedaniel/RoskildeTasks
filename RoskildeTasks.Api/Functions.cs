using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web.WebApi;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

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
    }
}
